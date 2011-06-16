﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamDev.Redis.Interface;
using System.ComponentModel;


namespace TeamDev.Redis.LanguageItems
{
  public static class TimeExtensions
  {
    [Description(CommandDescriptions.EXPIRE)]
    public static bool Expire(this IComplexItem item, int seconds)
    {      
      return item.Provider.ReadInt(item.Provider.SendCommand(RedisCommand.EXPIRE, item.KeyName, seconds.ToString())) == 1;
    }

    [Description(CommandDescriptions.PERSIST)]
    public static bool Persist(this IComplexItem item)
    {      
      return item.Provider.ReadInt(item.Provider.SendCommand(RedisCommand.EXPIRE, item.KeyName)) == 1;
    }

    //public static bool Expire(this IComplexItem item, DateTime time)
    //{
    //  item.Provider.InternalSendCommand(Provides.RedisCommand.EXPIRE, item.KeyName, time.ToUniversalTime);
    //  return item.Provider.ReadInt() == 1;
    //}

    [Description(CommandDescriptions.TTL)]
    public static int TTL(this IComplexItem item)
    {      
      return item.Provider.ReadInt(item.Provider.SendCommand(RedisCommand.TTL, item.KeyName));
    }
  }
}
