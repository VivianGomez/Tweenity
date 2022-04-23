using System.Net;
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
using static StringExtension;

public class SimulationObject {
    private const string kStart = "start";
    private const string kEnd = "end";

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
            PrintOnDebug(nodeTitle);
            return nodes [ nodeTitle ];
        }

        public Node GetStartNode() {
            
            try{
                UnityEngine.Assertions.Assert.IsNotNull( titleOfStartNode );
            }
            catch(Exception)
            {
                PrintError("NullException","Error al cargar el nodo inicial, asegúrese de tener un nodo start y un nodo end. \nSi los tiene, verifique que sus nodos tengan la estructura correcta");
            }
            
            return nodes [ titleOfStartNode ];
        }

        string ChangeDot(string parameters)
        {
            return parameters.Replace(".",",");
        }

        private string SetColorByType(string str)
        {
            string respuesta = str;
            if(str.Contains("start"))
            {
                respuesta = str.Color("green");
            }
            else if(str.Contains("end"))
            {
                respuesta = str.Color("red");
            }
            else if(str.Contains("dialogue"))
            {
                respuesta = str.Color("magenta");
            }
            else if(str.Contains("random"))
            {
                respuesta = str.Color("yellow");
            }
            else if(str.Contains("timeout"))
            {
                respuesta = str.Color("cyan");
            }
            else if(str.Contains("reminder"))
            {
                respuesta = str.Color("cyan");
            }
            else if(str.Contains("multiplechoice"))
            {
                respuesta = str.Color("orange");
            }

            return respuesta;
        }

        private void PrintError(string error, string msj, string link="https://www.youtube.com/watch?v=1zJVxYkMtjM")
        {
            Debug.LogError((error+": ").Bold().Color("red")+ msj+"\n"+"Para más información sobre los formatos consulte el "+"video de creación de grafos de simulación en Twine ".Link(link)+", o escriba en el "+"foro de la etapa de desarrollo ".Link("https://github.com/VivianGomez/Tweenity/discussions/17"));
        }

        private void PrintOnDebug(string msj)
        {
            if (debugging) Debug.Log(msj);
        }

        private bool IsValidTag(string tag)
        {
            return (tag.Equals("start") || tag.Equals("end") || tag.Equals("dialogue") || tag.Equals("random") 
                            || tag.Equals("reminder") || tag.Equals("multiplechoice") || tag.Equals("timeout"));
        }

        private bool ContainsSpecialCharacter(string tNodo, string textoCompleto, char sc, string msjUsoCorrecto, string msjUbicacionCorrecta)
        {
            
                int count = textoCompleto.Split(sc).Length - 1;
                if(textoCompleto.Contains(sc))
                {
                    if(count>1)
                    {
                        PrintError("NodeFormatError", "El nodo \""+ tNodo+ "\" tiene más de un caracter "+sc+". Tiene ("+count+") \n Por favor ingrese sólo un caracter "+sc+", y asegurese de usarlo "+ msjUbicacionCorrecta);
                        return false;
                    }
                }
                else
                {
                    PrintError("NodeFormatError","El nodo \""+ tNodo+ "\" no tiene el caracter "+sc+", el cual es esencial para "+msjUsoCorrecto+". \n Por favor ingrese el caracter "+sc+" "+msjUbicacionCorrecta);
                    return false;
                }
            

            return true;
        }

        private List<Action> CreateActions(string titleNode, string[] ActionData, Regex rg, string msjeTipoAccion)
        {
            List<Action> actionsList = new List<Action>();
            if(ActionData.Length>0)
            {
                PrintOnDebug("Se cargaron las siguientes acciones "+msjeTipoAccion+": ");
                for ( int k = 0; k < ActionData.Length; k++ ) 
                {
                    string curActionData = ActionData[k].Trim();
                    curActionData = Regex.Replace(curActionData, @"\;\s", ";");

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
                            curAction.actionParams = ChangeDot(splitSepParams[1].Substring(0, paramsEnd).Replace("\"", ""));
                            

                            PrintOnDebug("- GameObject: "+curAction.object2Action+" - Nombre función: "+curAction.actionName+" -  "+(curAction.actionParams==""?"Sin parámetros":" Parámetros: ")+curAction.actionParams);

                            actionsList.Add( curAction );
                        }
                        else
                        {
                            PrintError("NodeFormatError","En el nodo '"+titleNode+"' la acción "+curActionData+ " no tiene el formato esperado, verifique que los parámetros estén separados por ';' no por ',' ni por ' '(espacio en blanco) u otro separador \n Y recuerde que el formato es NombreGameObject.NombreFuncion(param1;...;paramn). Si ese no es el error,verifique que para números decimales esé usando '.' y no ','");
                        }
                    } 
                }
                PrintOnDebug("---");
            }

            return actionsList;           
        }

        private void VerifyActionsReminderTimeout(Node curNode, string tipoNodo, string metodo1, string parametros)
        {
            if(curNode.userActions.Count==0 || curNode.userActions.Count==1 || curNode.userActions.Count>2)
            {
                PrintError("NodeFormatError","El nodo '"+curNode.title+"' de tipo "+SetColorByType(tipoNodo)+" debe tener exactamente dos acciones en el bloque {}, pero sólo tiene "+ curNode.userActions.Count +"\nLa primera debería ser el llamado al método "+metodo1+" con sus respectivos parámetros ("+parametros+"). Y la segunda debe ser la acción de usuario esperada (sólo una)");
            }
            else if(curNode.userActions.Count==2)
            {
                Action reminder = curNode.userActions[0];
                string action1 = reminder.object2Action+"."+reminder.actionName;
                string action2 = curNode.userActions[1].object2Action+"."+curNode.userActions[1].actionName;

                if(!(action1.Equals(metodo1)) || (action2.Equals(metodo1)))
                {
                    PrintError("NodeFormatError","El nodo '"+curNode.title+"' de tipo "+SetColorByType(tipoNodo)+" debería tener como primera acción del bloque {...} a "+metodo1+" con sus respectivos parámetros ("+parametros+"). Sin embargo, se encontró "+action1+" en su lugar");
                }
            }       
        }

        private bool HaveTimeoutReponse(List<Response> responses, string pattern)
        {
            Regex rg = new Regex(pattern);
            int c = 0;
            for (int i = 0; i < responses.Count; i++)
            {
                if(rg.IsMatch(responses[i].displayText))
                {
                    c++;
                }
            }

            return c==1 ? true : false;
        }


        public void ParseTwineText( TextAsset twineText) {
            string text = twineText.text;
            string[] nodeData = text.Split(new string[] { "::" }, StringSplitOptions.None);
            string patternSimpleText = @"[A-Za-z0-9À-ÿ\u00f1\u00d1\(\)?,-¿!¡#$%&\/\\ ]+";
            //string patternCompleteFormat = @"(\[\[[\u00f1\u00d1\w+:.,;\(\)'\u0022'?,-¿!¡#$%&\/\\ ) ]+\]\](\r\n)+)+\{[\n\r\w+.\(\);,'\u0022' ]*[\n\r]*\}[\n\r]+\<[\n\r\w+.\(\),;'\u0022' ]*\>";

            const int kIndexOfContentStart = 4;
            PrintOnDebug("........................... EMPIEZA LA CARGA Y PARSING DEL GRAFO EN TEXTO ENTWEE .......................".Bold().Size(14));

            for ( int i = 0; i<nodeData.Length; i++ ) {
                if ( i < kIndexOfContentStart )
                    continue;

                PrintOnDebug(("......................................................................... Parsing nodo "+(i-3)+"  ..............................................................................").Bold());
                string currLineText = nodeData[i];
                bool tagsPresent = currLineText.IndexOf( "[" ) < currLineText.IndexOf( "\r\n" );
                int endOfFirstLine = currLineText.IndexOf( "\r\n" );

                // Extract Title
                int titleStart = 0;
                int titleEnd = tagsPresent
                    ? currLineText.IndexOf( "[" )
                    : endOfFirstLine;
                string title = currLineText.Substring(titleStart, titleEnd).Trim();
                PrintOnDebug("Título del nodo:  ".Bold()+title);

                // Extract Tags (if any)
                string tags = tagsPresent
                    ? currLineText.Substring( titleEnd + 1, (endOfFirstLine - titleEnd)-2)
                    : "";
                tags = tags.ToLower();

                if(!ContainsSpecialCharacter(title,currLineText.Substring(endOfFirstLine),'@',"dividir la parte "+"descriptiva".Italic()+" de la de "+"scripting".Italic(),"al finalizar la descripción y antes de la zona scripting")) return; 
                // Extract Responses, Message Text user and simulator actions
                string bodyNode = currLineText.Split('@')[1].Trim();

                //if(new Regex(patternCompleteFormat).IsMatch(bodyNode))
                //{

                if(!ContainsSpecialCharacter(title,bodyNode,'{',"especificar la apertura de las acciones de usuario","debajo de los caminos o nodos hijo")) return; 
                if(!ContainsSpecialCharacter(title,bodyNode,'}',"especificar el cierre de las acciones de usuario","al terminar de escribir sus acciones de usuario")) return; 
                string messageAndResponses = bodyNode.Substring( 0, bodyNode.IndexOf("{") );
                // Extract Message Text
                string message = messageAndResponses.Substring( 0, messageAndResponses.IndexOf("[[")).Trim();
                string responseText = messageAndResponses.Substring( message.Length).Trim();

                // Extract UserActions
                string userActionsText = bodyNode.Substring(bodyNode.IndexOf("{")+1);
                userActionsText = userActionsText.Substring(0,userActionsText.IndexOf("}")).Trim();
                //PrintOnDebug("El texto de las acciones de usuario son:  "+userActionsText);

                if(!ContainsSpecialCharacter(title,bodyNode,'<',"especificar la apertura de las acciones de simulador","debajo del cierre de las acciones de usuario '}'")) return; 
                if(!ContainsSpecialCharacter(title,bodyNode,'>',"especificar el cierre de las acciones de simulador","al terminar de escribir sus acciones de simulador")) return; 
                // Extract SimulatorActions
                string simulatorActionsText = bodyNode.Substring(bodyNode.IndexOf("<")+1);
                simulatorActionsText = simulatorActionsText.Substring(0,simulatorActionsText.IndexOf(">")).Trim();
                //PrintOnDebug("El texto de las acciones de simulador son:  "+simulatorActionsText);

                Node curNode = new Node();
                curNode.title = title;
                curNode.text = message;
                curNode.tags = new List<string>( tags.Split( new string [] { " " }, StringSplitOptions.None ) );
                
                foreach (var tag in curNode.tags)
                {
                    if(tag!="" && !IsValidTag(tag))
                    {
                        PrintError("NodeFormatError","En el nodo '"+title+"' el tag "+ tag + " no es un tipo de nodo válido, verifique su escritura, recuerde que los tags válidos son:\nstart, end, dialogue, random, reminder, multipleChoice y timeout");
                        return;
                    }
                }

                if(tags!="") {PrintOnDebug(("El nodo es de tipo(s): "+ SetColorByType(tags)).Bold());} else{PrintOnDebug("Es un nodo sin un tipo particular (sin tags)");}
                if (tags.Contains("dialogue")) PrintOnDebug("El mensaje del diálogo es:  "+message);

                if ( curNode.tags.Contains( kStart ) ) {
                    UnityEngine.Assertions.Assert.IsTrue( null == titleOfStartNode );
                    titleOfStartNode = curNode.title;
                }

                string pattern = @"^[A-Za-z]+\w+.[A-Za-z]+\w+\(['\u0022'?\w+. \-'\u0022'?;]*\)$";  
                Regex rg = new Regex(pattern);

                // user actions
                if(userActionsText!="")
                {
                    string[] ActionData = userActionsText.Trim().Split(new string [] { "\r\n" }, StringSplitOptions.None);
                    curNode.userActions = CreateActions(title, ActionData, rg, "de usuario");

                    if(curNode.userActions.Count == 1)
                    {
                        PrintOnDebug("En total el nodo tiene 1 acción de usuario");
                    }
                }
                else
                {
                    curNode.userActions = new List<Action>();
                    PrintOnDebug("El nodo no tiene acciones de usuario");
                }

                bool multipleChoiceType = curNode.tags.Contains("multiplechoice") && !curNode.tags.Contains("reminder") && !curNode.tags.Contains("timeout");
                // Verificación en la estructura de acciones dependiendo del tipo de nodo
                if(curNode.tags.Contains("reminder"))
                {
                    if (curNode.tags.Contains("multiplechoice")) 
                    {
                        PrintError("NodeFormatError","Hay un error en el formato del nodo '"+curNode.title+"', un nodo de tipo "+SetColorByType("reminder")+" no puede ser "+SetColorByType("multipleChoice")+"\nEs una funcionalidad no disponible por el momento.");
                    }
                    else
                    {
                        VerifyActionsReminderTimeout(curNode, "reminder", "Simulator.ShowReminder", "número de segundos a esperar; objeto del cual se espera la acción; un string vacío o con el nombre de un audio a reproducir");
                    }
                }
                else if(curNode.tags.Contains("timeout"))
                {
                    if (curNode.tags.Contains("multiplechoice")) 
                    {
                        PrintError("NodeFormatError","Hay un error en el formato del nodo '"+curNode.title+"', un nodo de tipo "+SetColorByType("timeout")+" no puede ser "+SetColorByType("multipleChoice")+"\nEs una funcionalidad no disponible por el momento.");
                    }
                    else
                    {
                        VerifyActionsReminderTimeout(curNode, "timeout", "Simulator.Timeout", "número de segundos a esperar");
                    }
                }
                else if(multipleChoiceType)
                {
                    if(curNode.userActions.Count>1)
                    {
                        PrintOnDebug("En total el nodo tiene "+curNode.userActions.Count+" acciones de usuario");
                    }
                    else
                    {
                        PrintError("NodeFormatError","El nodo '"+curNode.title+"' de tipo "+SetColorByType("multipleChoice")+" debe tener más de una acción de usuario, de lo contrario no debería tener dicho tipo");
                    }
                }
                else if(curNode.userActions.Count > 2)
                {
                    PrintError("IncorrectNodeType","El nodo '"+curNode.title+"' tiene "+curNode.userActions.Count+" acciones de usuario, debería ser un "+SetColorByType("multipleChoice"));
                }

                // simulator actions
                if(simulatorActionsText!="")
                {
                    string[] ActionDataSim = simulatorActionsText.Split(new string [] { "\r\n" }, StringSplitOptions.None);
                    curNode.simulatorActions = CreateActions(title, ActionDataSim, rg, "de simulador");

                    if(curNode.simulatorActions.Count == 1)
                    {
                        PrintOnDebug("En total el nodo tiene 1 acción de simulador");
                    }
                    else
                    {
                        PrintOnDebug("En total el nodo tiene "+curNode.simulatorActions.Count+" acciones de simulador");
                    }
                }
                else
                {
                    curNode.simulatorActions = new List<Action>();
                    PrintOnDebug("El nodo no tiene acciones de simulador");
                }

                // response messages
                curNode.responses = new List<Response>();
                //if ( !lastNode ) {
                    List<string> responseData = new List<string>(responseText.Split( new string [] { "\r\n" }, StringSplitOptions.None ));
                    PrintOnDebug("Se cargaron los siguientes caminos: ");
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

                        if(curNode.tags.Contains("random"))
                        {
                            pattern = @"^\d+:"+patternSimpleText;
                            rg = new Regex(pattern);

                            if(!rg.IsMatch(curResponse.displayText))
                            {
                                string error = "PathFormatError: Hay un error en el formato esperado para el camino '"+curResponse.displayText+"'\nComo el tipo de nodo es "+SetColorByType("random")+", se espera el formato #:titulo nodo hijo o camino, dónde # es el número del camino. Ej: 1:Abrir la puerta";
                                if(!(curResponse.displayText.Contains(':')))
                                { 
                                    PrintError("NodeFormatError","En el camino '"+curResponse.displayText+"' del nodo '"+curNode.title+"' falta el caracter ':'\n"+error);
                                    return;
                                }
                                else
                                {
                                    PrintError("NodeFormatError","En el camino '"+curResponse.displayText+"' del nodo '"+curNode.title+"' no se está siguiendo el formato, es posible que falte el número antes del caracter ':' o que no esté ordenado\n"+error);
                                    return;
                                }
                            }
                        }
                        else if(multipleChoiceType)
                        {
                            pattern = @"^[A-Za-z]+\w+.[A-Za-z]+\w+\(['\u0022'?\w+.'\u0022'?; ]*\):"+patternSimpleText;
                            rg = new Regex(pattern);
                            if(!rg.IsMatch(curResponse.displayText))
                            {
                                PrintError("PathFormatError","Hay un error en el formato esperado para el camino '"+curResponse.displayText+"' del nodo '"+curNode.title+"'\nComo el tipo de nodo es "+SetColorByType("multipleChoice")+", se espera el formato 'GameObject.NombreFuncion():titulo nodo hijo o camino', dónde GameObject.NombreFuncion() corresponde exáctamente a una de las acciones de usuario definidas en el bloque {...}.\nEj: Puerta.Abrir():Prender la luz");
                            }
                            else
                            {
                                string pathUA = curResponse.displayText.Split(':')[0];
                                if(!userActionsText.Contains(curResponse.displayText.Split(':')[0]))
                                {
                                    PrintError("PathFormatError","En el nodo '"+curNode.title+"', la acción de usuario "+pathUA+ " no es una de las acciones de usuario definidas en el bloque correspondiente, delimitado por '{}', es decir una de las siguientes:\n---\n"+ userActionsText +"\n----\nVerifique que esté escrita exáctamente igual que cómo se definió en dicho bloque");
                                }
                            }
                        }

                        PrintOnDebug("- [["+curResponse.displayText+"]]");
                        curNode.responses.Add( curResponse );
                    }
                    PrintOnDebug("En total el nodo tiene "+(curNode.responses.Count)+" camino (s).");

                    if(multipleChoiceType && curNode.responses.Count!=curNode.userActions.Count)
                    {
                        PrintError("NodeFormatError","El nodo '"+curNode.title+"' de tipo "+SetColorByType("multipleChoice")+", debería tener la misma cantidad de acciones de usuario y caminos. Pero tiene "+curNode.userActions.Count+" acciones de usuario y "+curNode.responses.Count+" caminos posibles");
                    }
                    if(curNode.tags.Contains("timeout"))
                    {
                        if ((!(curNode.responses.Count==2)) && !HaveTimeoutReponse(curNode.responses, @"^(timeout):"+patternSimpleText)) {PrintError("PathFormatError","Hay un error en el formato del nodo '"+curNode.title+"'\nComo el tipo de nodo es "+SetColorByType("timeout")+", se espera que hayan sólo dos caminos y que uno de ellos tenga el siguiente formato 'timeout:titulo nodo hijo o camino' Pero se encontraron los siguientes ("+curNode.responses.Count+")\n----\n"+responseText+"\n---\nVerifique que esté bien dicho formato, incluyendo la palabra reservada 'timeout' seguida de ':' y el título del nodo\n");};
                    }
                //}

                nodes [ curNode.title ] = curNode;
                //}
                //else
                //{
                //    PrintError("NodeFormatError","El nodo "+title+" no tiene la estructura esperada en la sección de scripting, recuerde que el orden de las secciones importa ([[]]{}<>) y debe ser:\n_________________\nMensaje diálogo (si es de dicho tipo)\n[[camino_1]]\n.\n.\n.\n[[camino_n]]\n{\nacciones de usuario \n}\n<\nacciones de simulador\n>\n_________________\nEl nodo leído tiene una estructura incorrecta: \n"+bodyNode);
                //}
            }
            PrintOnDebug("*************** TERMINA LA CARGA Y PARSING DEL GRAFO EN TEXTO ENTWEE ***************".Bold());
        }
    }
}
