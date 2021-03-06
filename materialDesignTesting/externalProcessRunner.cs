﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace materialDesignTesting
{
    class externalProcessRunner
    {
        // full path of python interpreter
        //string python = @"E:/Python37";
        string python = @"cmd.exe";
        // python app to call
        string myPythonApp = @" /c python C:/Auction.py";
        public externalProcessRunner() { }
        externalProcessRunner(string pythonPath, String scriptPath)
        {
            //validate and initialize the values
            if(!String.IsNullOrEmpty(pythonPath)&&String.IsNullOrEmpty(scriptPath))
            {
                python = pythonPath;
                myPythonApp = scriptPath;
            }
        }
        //remove
        /// <summary>
        ///a temp variable to hold all the parameters that are sent to the function
        /// </summary>
        //String[] args = { "n0", "m", "y", "z", "w" };
        Double[] args = { 1000, 0.25 , 0.25 , 0.25 , 0.25 };
        //end of remove

        /// <summary>
        /// A helper function that combines an array of strings to a single string
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private String convertArrToString(Double[] arr)
        {
            String str = "";
            foreach (Double Val in arr)
                str+= String.Format("{0} ", Val);
            return str;
        }

        // Create new process start info
        public bool isDone = false;
        /// <summary>
        /// the following method will call an external python script,
        /// and return an array of the JSON response.
        /// </summary>
        /// <returns></returns>
        public resultArrays runCmd()
        {
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo();
            myProcessStartInfo.FileName = python;
            String Arguments = String.Format("{0} {1}", myPythonApp, constructArgsFromUserInput());
            System.Console.Write(String.Format("{0} {1}", myPythonApp, constructArgsFromUserInput()));
            myProcessStartInfo.Arguments = Arguments;
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;

            ///check the method of input (file or manual) if manual convert to a list
            if(ViewsMediator.OpponentObservable.Count > 0)
            {
                foreach (listBoxVector lvb in ViewsMediator.OpponentObservable)
                    ViewsMediator.Opponent.Add(lvb.value);
            }

            using (Process process = Process.Start(myProcessStartInfo))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                    jsonHandler jh = new jsonHandler(result);
                    resultArrays ra = jh.deserialize();
                    return ra;
                }
            }
        }


        private String constructArgsFromUserInput()
        {
            String gameParams =" "+ Convert.ToString(ViewsMediator.n0);
            gameParams = gameParams + " " + Convert.ToString(ViewsMediator.m);
            gameParams = gameParams + " " + Convert.ToString(ViewsMediator.K);
            gameParams = gameParams + " " + Convert.ToString(ViewsMediator.N);
            gameParams = gameParams + " " + Convert.ToString(ViewsMediator.y);
            gameParams = gameParams + " " + Convert.ToString(ViewsMediator.z);
            gameParams = gameParams + " " + Convert.ToString(ViewsMediator.w);
            gameParams = gameParams + " " + Convert.ToString(ViewsMediator.isUserFirst);
            foreach (double val in ViewsMediator.Opponent)
                gameParams += " " + Convert.ToString(val);
            System.Console.WriteLine(gameParams);
            return gameParams;



        }
    }
}
