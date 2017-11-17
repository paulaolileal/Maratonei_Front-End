namespace Maratonei_xamarin.Models
{
    public class ComidaSerie: BaseDataObject
    {
        double quantidade;
        string serie;

        public double Quantidade { get => quantidade; set => SetProperty(ref quantidade, value); }
        public string Serie { get => serie; set => SetProperty(ref serie, value); }
    }
}