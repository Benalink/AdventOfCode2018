using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2018
{
    public static class DictionaryExtensions
    {
        //This was taken and adapted from https://stackoverflow.com/questions/16192906/net-dictionary-get-or-create-new
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) 
            where TValue : new()
        {
            if (!dict.TryGetValue(key, out TValue val))
            {
                val = new TValue();
                dict.Add(key, val);
            }

            return val;
        }
    }
}
