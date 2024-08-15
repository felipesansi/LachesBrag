using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LachesBrag.TagHelpers
{
    [HtmlTargetElement("email")]
    public class EmailTagHelper : TagHelper
    {
        public string Endereco { get; set; }
        public string Conteudo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Define o nome da tag HTML para "a"
            output.TagName = "a";

            // Constrói o atributo "href" com o prefixo "mailto:"
            var mailtoLink = $"mailto:{Endereco}";
            output.Attributes.SetAttribute("href", mailtoLink);

            // Define o conteúdo da tag "a"
            output.Content.SetContent(Conteudo);
        }
    }
}
