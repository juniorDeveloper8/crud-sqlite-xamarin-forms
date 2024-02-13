using SQLite;

namespace Crud_Sqlite.Model
{
    public class Cliente
    {
        [PrimaryKey, AutoIncrement] 
        public int ID { get; set; }
        public string nombre {  get; set; }
        public string apellido {  get; set; }
        public string correo { get; set; }
        public string psw {  get; set; }
        public bool estado {  get; set; }
    }
}
