using Abstract;
using Abstract.Interfaces;
using Data.ValueObject;
using Enums;

namespace Command.SaveLoadCommands
{
    public class LoadGameCommand
    {
        public T Execute<T>(string key, int uniqueId) where T : ISaveableEntity
        {
            string _path = key + uniqueId.ToString() + ".es3";
            
            if (ES3.FileExists(_path))
            {
                if (ES3.KeyExists(key,_path))
                {
                    T objectToReturn = ES3.Load<T>(key,_path);
                    
                    return objectToReturn;
                }
                else
                {
                    return default(T); 
                }
            }
            return default(T);
        }

       
    }
}