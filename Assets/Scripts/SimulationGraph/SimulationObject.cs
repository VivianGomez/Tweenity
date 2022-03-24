//
//  SimulationObject.cs
//  Tweenity
//
//  Modified by Vivian Gómez, inspired by the work of Matthew Ventures (https://www.mrventures.net/all-tutorials/converting-a-twine-story-to-unity)
//  Copyright © 2021 Vivian Gómez - Pablo Figueroa - Universidad de los Andes
//

using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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

        bool debugging = false;

        public SimulationScript( TextAsset twineText, bool debug ) {
            nodes = new Dictionary<string, Node>();
            debugging = debug;
            ParseTwineText(twineText);
        }

        public Node GetNode( string nodeTitle ) {
            Debug.Log(nodeTitle);
            return nodes [ nodeTitle ];
        }

        public Node GetStartNode() {
            
            try{
                UnityEngine.Assertions.Assert.IsNotNull( titleOfStartNode );
            }
            catch(Exception)
            {
                Debug.LogError("NullException: Error al cargar el nodo inicial, verifique que tiene la estructura correcta");
            }
            
            return nodes [ titleOfStartNode ];
        }

        private void PrintOnDebug(string msj)
        {
            if (debugging) Debug.Log(msj);
        }

        private bool IsValidTag(string tag)
        {
            return (tag.Equals("START") || tag.Equals("END") || tag.Equals("dialogue") || tag.Equals("random") 
                            || tag.Equals("reminder") || tag.Equals("multipleChoice") || tag.Equals("timeOut"));
        }

        private bool ContainsSpecialCharacter(string tNodo, string textoCompleto, char sc, string msjUsoCorrecto, string msjUbicacionCorrecta)
        {
            if(debugging)
            {
                int count = textoCompleto.Split(sc).Length - 1;
                if(textoCompleto.Contains(sc))
                {
                    if(count>1)
                    {
                        Debug.LogError("NodeFormatError: El nodo \""+ tNodo+ "\" tiene más de un caracter "+sc+". Tiene ("+count+") \n Por favor ingrese sólo un caracter "+sc+", y asegurese de usarlo "+ msjUbicacionCorrecta);
                        return false;
                    }
                }
                else
                {
                    Debug.LogError("NodeFormatError: El nodo \""+ tNodo+ "\" no tiene el caracter "+sc+", el cual es esencial para "+msjUsoCorrecto+". \n Por favor ingrese el caracter "+sc+" "+msjUbicacionCorrecta);
                    return false;
                }
            }

            return true;
        }

        private List<Action> CreateActions(string[] ActionData, Regex rg, string msjeTipoAccion)
        {
            List<Action> actionsList = new List<Action>();
            for ( int k = 0; k < ActionData.Length; k++ ) 
            {
                string curActionData = ActionData[k].Trim();
                curActionData = Regex.Replace(curActionData, @"\s", "");
                if(curActionData!="")
                {
                    if(rg.IsMatch(curActionData))
                    {
                        Action curAction = new Action();
            
                        string[] splitSepParams = curActionData.Split('(');
                        string[] splitObjectName = splitSepParams[0].Split('.');

                        curAction.object2Action = splitObjectName[0];
                        curAction.actionName = splitObjectName[1];

                        int paramsEnd = splitSepParams[1].IndexOf(")");
                        curAction.actionParams = splitSepParams[1].Substring(0, paramsEnd).Replace("\"", "");

                        PrintOnDebug("Se detectó la acción "+msjeTipoAccion+ ": \nGameObject: "+curAction.object2Action+" - Nombre función: "+curAction.actionName+" - Parámetros: "+curAction.actionParams);

                        actionsList.Add( curAction );
                    }
                    else
                    {
                        Debug.LogError("NodeFormatError: La acción "+curActionData+ " no tiene el formato esperado, verifique que los parámetros estén separados por ';' no por ',' \n Y recuerde que el formato es NombreGameObject.NombreFuncion(param1;...;paramn)");
                    }
                } 
            }

            return actionsList;           
        }

        public void ParseTwineText( TextAsset twineText) {
            string text = twineText.text;
            string[] nodeData = text.Split(new string[] { "::" }, StringSplitOptions.None);

            const int kIndexOfContentStart = 4;
            PrintOnDebug("*************** EMPIEZA LA LECTURA Y PARSING DEL TEXTO ENTWEE ***************");

            for ( int i = 0; i<nodeData.Length; i++ ) {
                if ( i < kIndexOfContentStart )
                    continue;

                // Note: tags are optional
                // Normal Format: "NodeTitle [Tags, comma, seperated] \r\n Message Text \r\n [[Response One]] \r\n [[Response Two]]"
                // No-Tag Format: "NodeTitle \r\n Message Text \r\n [[Response One]] \r\n [[Response Two]]"
                PrintOnDebug("................. Parsing nodo "+(i-3)+"  .................");
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
                PrintOnDebug("Título del nodo:  "+title);

                // Extract Tags (if any)
                string tags = tagsPresent
                    ? currLineText.Substring( titleEnd + 1, (endOfFirstLine - titleEnd)-2)
                    : "";

                if(!ContainsSpecialCharacter(title,currLineText.Substring(endOfFirstLine),'@',"dividir la parte _descriptiva_ de la de _scripting_","al finalizar la descripción y antes de la zona scripting")) return; 
                // Extract Responses, Message Text user and simulator actions
                string bodyNode = currLineText.Split('@')[1].Trim();

                if(!ContainsSpecialCharacter(title,currLineText.Substring(endOfFirstLine),'{',"especificar la apertura de las acciones de usuario","debajo de los caminos o nodos hijo")) return; 
                if(!ContainsSpecialCharacter(title,currLineText.Substring(endOfFirstLine),'}',"especificar el cierre de las acciones de usuario","al terminar de escribir sus acciones de usuario")) return; 
                string messageAndResponses = bodyNode.Substring( 0, bodyNode.IndexOf("{") );
                // Extract Message Text
                string message = messageAndResponses.Substring( 0, messageAndResponses.IndexOf("[[")).Trim();
                if (debugging & tags.Contains("dialogue")) Debug.Log("El mensaje del diálogo es:  "+message);

                string responseText = messageAndResponses.Substring( message.Length).Trim();

                // Extract UserActions
                string userActionsText = bodyNode.Substring(bodyNode.IndexOf("{")+1);
                userActionsText = userActionsText.Substring(0,userActionsText.IndexOf("}")).Trim();
                //PrintOnDebug("El texto de las acciones de usuario son:  "+userActionsText);

                // Extract SimulatorActions
                string simulatorActionsText = bodyNode.Substring(bodyNode.IndexOf("<")+1);
                simulatorActionsText = simulatorActionsText.Substring(0,simulatorActionsText.IndexOf(">")).Trim();
                //PrintOnDebug("El texto de las acciones de simulador son:  "+simulatorActionsText);

                Node curNode = new Node();
                curNode.title = title;
                curNode.text = message;
                curNode.tags = new List<string>( tags.Split( new string [] { " " }, StringSplitOptions.None ) );
                
                if(debugging)
                {
                    foreach (var tag in curNode.tags)
                    {
                        if(tag!="" && !IsValidTag(tag))
                        {
                            Debug.LogError("NodeFormatError: El tag "+ tag + " no es un tipo de nodo válido, verifique su escritura, recuerde que los tags válidos son:\nSTART, END, dialogue, random, reminder, multipleChoice y timeOut");
                            return;
                        }
                    }
                }

                if ( curNode.tags.Contains( kStart ) ) {
                    UnityEngine.Assertions.Assert.IsTrue( null == titleOfStartNode );
                    titleOfStartNode = curNode.title;
                }

                string pattern = @"^[A-Za-z]*\w*.[A-Za-z]*\w*\(['\u0022'?\w+.'\u0022'?; ]*\)$";  
                Regex rg = new Regex(pattern);

                // user actions
                string[] ActionData = userActionsText.Trim().Split(new string [] { "\r\n" }, StringSplitOptions.None);
                if(ActionData.Length>0)
                {
                    curNode.userActions = CreateActions(ActionData, rg, "de usuario");
                    PrintOnDebug("Este nodo tiene "+curNode.userActions.Count+" acciones de usuario");
                }

                // simulator actions
                string[] ActionDataSim = simulatorActionsText.Split(new string [] { "\r\n" }, StringSplitOptions.None);
                if(ActionDataSim.Length>0)
                {
                    curNode.simulatorActions = CreateActions(ActionDataSim, rg, "de simulador");
                    PrintOnDebug("Este nodo tiene "+curNode.simulatorActions.Count+" acciones de simulador");
                }
                

                // response messages
                curNode.responses = new List<Response>();
                //if ( !lastNode ) {
                    List<string> responseData = new List<string>(responseText.Split( new string [] { "\r\n" }, StringSplitOptions.None ));
                    PrintOnDebug("Este nodo tiene:  "+(responseData.Count)+" camino (s).");
                    for ( int k = responseData.Count-1; k >= 0; k-- ) {
                        string curResponseData = responseData[k];
                        //PrintOnDebug("El camino:  "+k+ " es: "+ curResponseData);

                        if ( string.IsNullOrEmpty( curResponseData )) {
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
            PrintOnDebug("*************** TERMINA LA LECTURA Y PARSING DEL TEXTO ENTWEE ***************");
        }
    }
}
