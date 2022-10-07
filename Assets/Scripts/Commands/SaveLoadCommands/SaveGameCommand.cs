using Abstract.Interfaces;

namespace Command.SaveLoadCommands
{
    public class SaveGameCommand
    {
        public void Execute<T>(T _dataToSave, int _uniqueID) where T : ISaveableEntity
        {   
            string _path =_dataToSave.GetKey() + _uniqueID.ToString() + ".es3";

            string _dataKey = _dataToSave.GetKey();
            
            if (!ES3.FileExists(_path))
            {
                ES3.Save(_dataKey,_dataToSave,_path);
            }

            ES3.Save(_dataKey,_dataToSave,_path);
        }
    }
}