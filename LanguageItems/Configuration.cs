﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamDev.Redis.LanguageItems
{
  public class Configuration
  {
    public string Host { get; set; }
    public int Port { get; set; }

    public string UserName { get; set; }
    public string Password { get; set; }

    public string DataBase { get; set; }
    public int SendTimeout { get; set; }

    public Configuration()
    {
      Host = "localhost";
      Port = 6379;
      SendTimeout = -1;
    }
  }
}
