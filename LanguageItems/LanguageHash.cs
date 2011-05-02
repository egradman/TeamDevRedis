﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamDev.Redis.Interface;

namespace TeamDev.Redis.LanguageItems
{
  public class LanguageHash : ILanguageItem, IComplexItem
  {
    internal string _name;
    internal RedisDataAccessProvider _provider;

    public void Clear()
    {
      _provider.InternalSendCommand(RedisCommand.DEL, _name);
      _provider.WaitComplete();
    }

    public string this[string field]
    {
      get
      {
        _provider.InternalSendCommand(RedisCommand.HGET, _name, field);
        return _provider.ReadString();
      }
      set
      {
        _provider.InternalSendCommand(RedisCommand.HSET, _name, field, value);
        _provider.WaitComplete();
      }
    }

    public string Get(string field)
    {
      _provider.InternalSendCommand(RedisCommand.HGET, _name, field);
      return _provider.ReadString();
    }

    public bool Set(string field, string value)
    {
      _provider.InternalSendCommand(RedisCommand.HSET, _name, field, value);
      return _provider.ReadInt() == 1;
    }

    public KeyValuePair<string, string>[] Items
    {
      get
      {
        _provider.InternalSendCommand(RedisCommand.HGETALL, _name);
        var result = _provider.ReadMultiString();

        var values = new List<KeyValuePair<string, string>>();
        if (result != null)
        {
          if (result.Length % 2 > 0) throw new InvalidOperationException("Invalid number of results");

          for (int x = 0; x < result.Length; x += 2)
            values.Add(new KeyValuePair<string, string>(result[x], result[x + 1]));

        }
        return values.ToArray();
      }
    }

    public string[] Keys
    {
      get
      {
        _provider.InternalSendCommand(RedisCommand.HKEYS, _name);
        return _provider.ReadMultiString();
      }
    }

    public string[] Values
    {
      get
      {
        _provider.InternalSendCommand(RedisCommand.HVALS, _name);
        return _provider.ReadMultiString();
      }
    }

    public bool ContainsKey(string key)
    {
      _provider.InternalSendCommand(RedisCommand.HEXISTS, _name, key);
      return _provider.ReadInt() == 1;
    }

    public bool Delete(string field)
    {
      _provider.InternalSendCommand(RedisCommand.HDEL, _name, field);
      return _provider.ReadInt() == 1;
    }

    public void Set(IDictionary<string, string> datas)
    {
      _provider.InternalSendCommand(RedisCommand.HMSET, datas, _name);
      _provider.WaitComplete();
    }

    public string[] Get(params string[] keys)
    {
      List<string> args = new List<string>();
      args.Add(_name);
      args.AddRange(keys);

      _provider.InternalSendCommand(RedisCommand.HMSET, args.ToArray());
      return _provider.ReadMultiString();
    }

    public int Lenght
    {
      get
      {
        _provider.InternalSendCommand(RedisCommand.HLEN, _name);
        return _provider.ReadInt();
      }
    }

    void ILanguageItem.Configure(string name, RedisDataAccessProvider provider)
    {
      _name = name;
      _provider = provider;
    }

    string IComplexItem.KeyName
    {
      get { return _name; }
    }

    RedisDataAccessProvider IComplexItem.Provider
    {
      get { return _provider; }
    }
  }


}
