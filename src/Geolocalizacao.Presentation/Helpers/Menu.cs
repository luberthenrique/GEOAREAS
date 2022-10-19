namespace Geolocalizacao.Presentation.Helpers
{
    public class Menu
    {
        public string Controller { get; set; }
        public string Acao { get; set; }
        public bool Crud { get; set; }
        public bool AbrirEmNovaTab { get; set; } = false;
        public string GetPath()
        {
            return $"/{Controller.Replace("Controller", "")}/{Acao}";
        }

    }
}