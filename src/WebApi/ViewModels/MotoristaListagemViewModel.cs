using Newtonsoft.Json;
using PagedList.Core;

namespace WebApi.ViewModels
{
    public class MotoristaListagemViewModel
    {
        [JsonProperty("itens")]
        public StaticPagedList<CadastroMotoristaViewModel> Itens { get; internal set; }
        [JsonProperty("pageNumber")]
        public int PageNumber { get; set; }
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
        [JsonProperty("sorteBy")]
        public string SortBy { get; set; }

        public MotoristaListagemViewModel()
        {
            this.PageNumber = 1;
            this.PageSize = 20;
            this.SortBy = "";
        }
    }
}
