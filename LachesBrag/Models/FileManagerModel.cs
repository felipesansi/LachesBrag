namespace LachesBrag.Models
{
    public class FileManagerModel
    {
        public FileInfo[] Files {  get; set; } //Files usado para tratar os arquivos
        public IFormFile IFormsFile { get; set; } //IFormFile é a interface que  permite enviar arquivos
        public List<IFormFile> IFormsFiles { get; set; } // lista de aquivos
        public  string PathImagemProduto { get; set; } // nome da pasta onde vai armazenar arquvos

    }
}
