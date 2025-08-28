namespace indexer
{
    internal class index_value
    {
        //array to store 3 colours
        string[] colours = new string[3];

        //indexer method to get and set the value
        public string this[int index]
        {
            //get and return the value at index
            get { return colours[index]; }
            //set value at index
            set { colours[index] = value; }
        }

        public index_value()
        {
            
        }
    }
}