//
//  SimulationObject.cs
//  Tweenity
//
//  Modified by Vivian Gómez, inspired by the work of Matthew Ventures (https://www.mrventures.net/all-tutorials/converting-a-twine-story-to-unity)
//  Copyright © 2021 Vivian Gómez - Pablo Figueroa - Universidad de los Andes
//

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationObject {
    private const string kStart = "START";
    private const string kEnd = "END";

    public struct Response {
        public string displayText;
        public string destinationNode;

        public Response( string display, string destination ) {
            displayText = display;
            destinationNode = destination;
        }
    }

    public struct Action {
        public string object2Action;
        public string actionName;
        public string actionParams;

        public Action( string objectN, string nameP, string paramsP) {
            object2Action = objectN;
            actionName = nameP;
            actionParams = paramsP;
        }
    }

    public class Node {
        public string title;
        public string text;
        public List<string> tags;
        public List<Action> userActions;
        public List<Action> simulatorActions;
        public List<Response> responses;
        public string audio;

        internal bool IsEndNode() {
            return tags.Contains( kEnd );
        }

        // TODO proper override
        public string Print() {
            return "";//string.Format( "Node {  Title: '%s',  Tag: '%s',  Text: '%s'}", title, tag, text );
        }

    }

    public class SimulationScript {
        string title;
        Dictionary<string, Node> nodes;
        string titleOfStartNode;
        public SimulationScript( TextAsset twineText ) {
            nodes = new Dictionary<string, Node>();
            ParseTwineText( twineText );
        }

        public Node GetNode( string nodeTitle ) {
            Debug.Log(nodeTitle);
            return nodes [ nodeTitle ];
        }

        public Node GetStartNode() {
            Debug.Log(titleOfStartNode);
            UnityEngine.Assertions.Assert.IsNotNull( titleOfStartNode );
            return nodes [ titleOfStartNode ];
        }

        public void ParseTwineText( TextAsset twineText ) {
            string text = twineText.text;
            string[] nodeData = text.Split(new string[] { "::" }, StringSplitOptions.None);

            const int kIndexOfContentStart = 4;
            for ( int i = 0; i<nodeData.Length; i++ ) {
                if ( i < kIndexOfContentStart )
                    continue;

                // Note: tags are optional
                // Normal Format: "NodeTitle [Tags, comma, seperated] \r\n Message Text \r\n [[Response One]] \r\n [[Response Two]]"
                // No-Tag Format: "NodeTitle \r\n Message Text \r\n [[Response One]] \r\n [[Response Two]]"
                string currLineText = nodeData[i];
                bool tagsPresent = currLineText.IndexOf( "[" ) < currLineText.IndexOf( "\r\n" );
                int endOfFirstLine = currLineText.IndexOf( "\r\n" );
                //Debug.Log("CUR LINE: ---" + currLineText);
                // Extract Title
                int titleStart = 0;
                int titleEnd = tagsPresent
                    ? currLineText.IndexOf( "[" )
                    : endOfFirstLine;
                string title = currLineText.Substring(titleStart, titleEnd).Trim();
                //Debug.Log("BODY title  "+title);
                // Extract Tags (if any)
                string tags = tagsPresent
                    ? currLineText.Substring( titleEnd + 1, (endOfFirstLine - titleEnd)-2)
                    : "";
                //Debug.Log("BODY tags  "+tags);

                // Extract Responses, Message Text user and simulator actions
                string bodyNode = currLineText.Split('@')[1].Trim();
                //Debug.Log("BODY NODE  "+bodyNode);

                string messageAndResponses = bodyNode.Substring( 0, bodyNode.IndexOf("{") );
                //Debug.Log("MESSAGE & RESPONSES  "+messageAndResponses);
                //Debug.Log("MESSAGE & RESPONSES  "+messageAndResponses.Length);
                // Start Responses
                /**
                int startOfResponses = -1;
                int startOfResponseDestinations = messageAndResponses.IndexOf( "[[" );
                bool lastNode = (startOfResponseDestinations == -1);
                if ( lastNode )
                    startOfResponses = messageAndResponses.Length;
                else {
                    // Last new line before "[["
                    startOfResponses = messageAndResponses.Substring( 0, startOfResponseDestinations ).LastIndexOf( "\r\n" );
                }
                */
                // End Responses

                // Extract Message Text
                string message = messageAndResponses.Substring( 0, messageAndResponses.IndexOf("[[")).Trim();
                //Debug.Log("MESSAGE  "+message); 

                string responseText = messageAndResponses.Substring( message.Length);
                //Debug.Log("RESPONSE  "+responseText);

                // Extract UserActions
                string userActionsText = bodyNode.Substring(bodyNode.IndexOf("{")+1);
                userActionsText = userActionsText.Substring(0,userActionsText.IndexOf("}")).Trim();
                //Debug.Log("USER ACTIONS  "+ userActionsText);

                // Extract SimulatorActions
                string simulatorActionsText = bodyNode.Substring(bodyNode.IndexOf("<")+1);
                simulatorActionsText = simulatorActionsText.Substring(0,simulatorActionsText.IndexOf(">")).Trim();
                //Debug.Log("SIMULATOR ACTIONS  "+ simulatorActionsText);


                Node curNode = new Node();
                curNode.title = title;
                curNode.text = message;
                curNode.tags = new List<string>( tags.Split( new string [] { " " }, StringSplitOptions.None ) );
                //curNode.simulatorActions = simulatorActionsText.Split(new string [] { "\r\n" }, StringSplitOptions.None);

                if ( curNode.tags.Contains( kStart ) ) {
                    UnityEngine.Assertions.Assert.IsTrue( null == titleOfStartNode );
                    titleOfStartNode = curNode.title;
                }

                // user actions
                curNode.userActions = new List<Action>();
                string[] ActionData = userActionsText.Split(new string [] { "\r\n" }, StringSplitOptions.None);
                if(ActionData.Length>0)
                {
                    for ( int k = 0; k < ActionData.Length; k++ ) 
                    {
                        string curActionData = ActionData[k];
                        if(curActionData!="")
                        {
                            Action curAction = new Action();

                            string[] splitSepParams = curActionData.Split('(');
                            string[] splitObjectName = splitSepParams[0].Split('.');

                            curAction.object2Action = splitObjectName[0];
                            //Debug.Log("OBJECT = "+curAction.object2Action);
                            
                            curAction.actionName = splitObjectName[1];
                            //Debug.Log("NAME = "+curAction.actionName);

                            int paramsEnd = splitSepParams[1].IndexOf(")");
                            curAction.actionParams = splitSepParams[1].Substring(0, paramsEnd).Replace("\"", "");;
                            //Debug.Log("PARAMS = "+curAction.actionParams);

                            curNode.userActions.Add( curAction );
                        }
                    }
                }

                // simulator actions
                curNode.simulatorActions = new List<Action>();
                string[] ActionDataSim = simulatorActionsText.Split(new string [] { "\r\n" }, StringSplitOptions.None);
                if(ActionDataSim.Length>0)
                {
                    for ( int k = 0; k < ActionDataSim.Length; k++ ) 
                    {
                        string curActionDataSim = ActionDataSim[k];
                        if(curActionDataSim!="")
                        {
                            Action curAction = new Action();

                            string[] splitSepParams = curActionDataSim.Split('(');
                            string[] splitObjectName = splitSepParams[0].Split('.');

                            curAction.object2Action = splitObjectName[0];
                            //Debug.Log("OBJECT = "+curAction.object2Action);
                            
                            curAction.actionName = splitObjectName[1];
                            //Debug.Log("NAME = "+curAction.actionName);

                            int paramsEnd = splitSepParams[1].IndexOf(")");
                            curAction.actionParams = splitSepParams[1].Substring(0, paramsEnd).Replace("\"", "");;
                            //Debug.Log("PARAMS = "+curAction.actionParams);

                            curNode.simulatorActions.Add( curAction );
                        }
                    }
                }
                

                // response messages
                curNode.responses = new List<Response>();
                //if ( !lastNode ) {
                    List<string> responseData = new List<string>(responseText.Split( new string [] { "\r\n" }, StringSplitOptions.None ));
                    //Debug.Log("RT: ---" + responseText);
                    for ( int k = responseData.Count-1; k >= 0; k-- ) {
                        string curResponseData = responseData[k];

                        if ( string.IsNullOrEmpty( curResponseData ) ) {
                            responseData.RemoveAt( k );
                            continue;
                        }

                        // If message-less, then destination is the message
                        Response curResponse = new Response();
                        int destinationStart = curResponseData.IndexOf( "[[");
                        int destinationEnd = curResponseData.IndexOf( "]]");
                        string destination = curResponseData.Substring(destinationStart + 2, (destinationEnd - destinationStart)-2);
                        curResponse.destinationNode = destination;
                        if ( destinationStart == 0 )
                            curResponse.displayText = destination;
                        else
                            curResponse.displayText = curResponseData.Substring( 0, destinationStart );
                        curNode.responses.Add( curResponse );
                    }
                //}

                nodes [ curNode.title ] = curNode;
            }
        }
    }
}
