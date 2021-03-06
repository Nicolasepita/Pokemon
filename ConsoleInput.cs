﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace Pokemon
{
    public class ConsoleInput
    {
        private List<string> msgs = new List<string>();

        public void Start_input_reading()
        {
            string s = "";
            while (string.IsNullOrEmpty(s))
            {
                s = Console.ReadLine();
            }
            s.ToLower();
            if (s == "stop")
            {
                Program.finish = true;
                return;
            }
            msgs.Add(s);
        }

        public string read_next_msg()
        {
            string s = "";
            if (msgs.Count != 0)
            {
                s = msgs[0];
                msgs.RemoveAt(0);
            }
            return s;
        }
        
        public List<string> Msgs => msgs;

        public void Clear()
        {
            msgs.Clear();
        }

        public bool Remove(string item)
        {
            return msgs.Remove(item);
        }

        public int Count => msgs.Count;
    }
}