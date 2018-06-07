using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using WebFc.Wappa.Teste.Base.EF;
using WebFc.Wappa.Teste.Base.EF.Repositorio;
using WebFC.Wappa.Teste.Base.Core.Interface;
using WebFC.Wappa.Teste.Base.Core.Repositorios;
using WebFC.Wappa.Teste.Base.Core.Services;

namespace WebFC.Wappa.Teste.DI.Unity
{
    public class Resolver : IDisposable
    {
        protected Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(() =>
        {
            IUnityContainer c = new UnityContainer();
            RegisterTypes(c);
            return c;
        });

        public Resolver()
        {
        }

        private Resolver(Lazy<IUnityContainer> container)
        {
            _container = container;
        }

        public object Resolve(Type type)
        {
            return _container.Value.Resolve(type);
        }

        public object Resolve(Type type, string name)
        {
            return _container.Value.Resolve(type, name);
        }

        public T Resolve<T>()
        {
            return _container.Value.Resolve<T>();
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            return _container.Value.ResolveAll(type);
        }

        public Resolver CreateChild()
        {
            return new Resolver(
                new Lazy<IUnityContainer>(() => { return _container.Value.CreateChildContainer(); })
            );
        }


        private static void RegisterTypes(IUnityContainer container)
        {

            //Repositorios
            //container.RegisterType<FabricaContext>(new InjectionConstructor("FabricaContext"));

            container.RegisterType<MotoristasContext>(new InjectionConstructor("MotoristasContext"));

            container.RegisterType<IMotoristasRepositorio, MotoristasRepositorio<MotoristasContext>>();
            container.RegisterType<ICarrosRepositorio, CarrosRepositorio<MotoristasContext>>();
            //container.RegisterType<ICupomRepository, CupomRepository<FabricaContext>>();
            //container.RegisterType<INumerosDaSorteRepository, NumerosDaSorteRepository<FabricaContext>>();
            //container.RegisterType<IPremioInstantaneoRepository, PremioInstantaneoRepository<FabricaContext>>();
            //container.RegisterType<IConfiguracoesPremioRepository, ConfiguracoesPremiosRepository<FabricaContext>>();
            //container.RegisterType<INumerosSorteadosRepository, NumerosSorteadosRepository<FabricaContext>>();
            //container.RegisterType<ICampanhaVisaoRepository, CampanhaVisaoRepository<FabricaContext>>();
            //container.RegisterType<IRelatorioUsuariosRepository, RelatorioUsuarioRepository<FabricaContext>>();
            //container.RegisterType<IRelatorioPremioInstantaneoRepository, RelatorioPremioInstantaneoRepository<FabricaContext>>();
            //container.RegisterType<IUsuariosPorDiaRepository, UsuariosPorDiaRepository<FabricaContext>>();
            //container.RegisterType<IRelatorioNumerosDaSorteRepository, RelatorioNumerosDaSorteRepository<FabricaContext>>();
            //container.RegisterType<ICuponsPorDiaRepository, CuponsPorDiaRepository<FabricaContext>>();
            //container.RegisterType<INumerosPorDiaRepository, NumerosPorDiaRepository<FabricaContext>>();
            //container.RegisterType<IPremiosPorDiaRepository, PremiosPorDiaRepository<FabricaContext>>();
            //container.RegisterType<ITicketMedioPorDiaRepository, TicketMedioPorDiaRepository<FabricaContext>>();
            //container.RegisterType<IPremiadosPorDiaRepository, PremiadosPorDiaRepository<FabricaContext>>();
            //container.RegisterType<IUsuariosPorSemanaRepository, UsuariosPorSemanaRepository<FabricaContext>>();
            //container.RegisterType<IRelatorioCuponsRepository, RelatorioCuponsRepository<FabricaContext>>();
            //container.RegisterType<ICuponsPorSemanaRepository, CuponsPorSemanaRepository<FabricaContext>>();
            //container.RegisterType<IPremiosPorSemanaRepository, PremiosPorSemanaRepository<FabricaContext>>();
            //container.RegisterType<INumerosPorSemanaRepository, NumerosPorSemanaRepository<FabricaContext>>();
            //container.RegisterType<ITicketMedioPorSemanaRepository, TicketMedioPorSemanaRepository<FabricaContext>>();
            //container.RegisterType<IPremiadosPorSemanaRepository, PremiadosPorSemanaRepository<FabricaContext>>();
            //container.RegisterType<IRetificacaoCuponsRepository, RetificacaoCuponsRepository<FabricaContext>>();


            //container.RegisterType<IAcessoClaimRepository, AcessoActionRepository<SCContext>>();
            //container.RegisterType<IAcessoMenuRepository, AcessoMenuRepository<SCContext>>();
            //container.RegisterType<IAcompanhanteRepository, AcompanhanteRepository<SCContext>>();
            //container.RegisterType<IAcompanhanteCampoConviteAcompanhanteConteudoRepository, AcompanhanteCampoConviteAcompanhanteConteudoRepository<SCContext>>();

            //container.RegisterType<IAcompanhanteOcorrenciaRepository, AcompanhanteOcorrenciaRepository<SCContext>>();
            //container.RegisterType<IAcompanhanteOcorrenciaCamposRepository, AcompanhanteOcorrenciaCamposRepository<SCContext>>();
            //container.RegisterType<IAplicacaoRepository, AplicacaoRepository<SCContext>>();
            //container.RegisterType<IAssociadoConvidadoConviteRepository, AssociadoConvidadoConviteRepository<SCContext>>();
            //container.RegisterType<IAssociadoConvidadoConviteOcorrenciaRepository, AssociadoConvidadoConviteOcorrenciaRepository<SCContext>>();

            //container.RegisterType<IAreaRepository, AreaRepository<SCContext>>();
            //container.RegisterType<IClaimRepository, ClaimRepository<SCContext>>();
            //container.RegisterType<ICotaConviteRepository, CotaConviteRepository<SCContext>>();
            //container.RegisterType<ICotaConviteOcorrenciaRepository, CotaConviteOcorrenciaRepository<SCContext>>();
            //container.RegisterType<ICotaGeralRepository, CotaGeralRepository<SCContext>>();
            //container.RegisterType<IConviteRepository, ConviteRepository<SCContext>>();
            //container.RegisterType<IConviteConfirmacaoDadosRepository, ConviteConfirmacaoDadosRepository<SCContext>>();
            //container.RegisterType<IConviteGeralRepository, ConviteGeralRepository<SCContext>>();
            //container.RegisterType<IConviteConvidadoRepository, ConviteConvidadoRepository<SCContext>>();
            //container.RegisterType<IConviteConvidadoReportRepository, ConviteConvidadoReportRepository<SCContext>>();
            //container.RegisterType<IConviteConvidadosGeralRepository, ConviteConvidadosGeralRepository<SCContext>>();
            //container.RegisterType<IConviteOcorrenciaRepository, ConviteOcorrenciaRepository<SCContext>>();
            //container.RegisterType<IConviteTotalDistribuidoRepository, ConviteTotalDistribuidoRepository<SCContext>>();
            //container.RegisterType<IConvidadoRepository, ConvidadoRepository<SCContext>>();
            //container.RegisterType<IConvidadoCampoConviteConteudoRepository, ConvidadoCampoConviteConteudoRepository<SCContext>>();
            //container.RegisterType<IConvidadoOcorrenciaRepository, ConvidadoOcorrenciaRepository<SCContext>>();
            //container.RegisterType<IConvidadoOcorrenciaCamposRepository, ConvidadoOcorrenciaCamposRepository<SCContext>>();
            //container.RegisterType<ICampoConviteRepository, CampoConviteRepository<SCContext>>();
            //container.RegisterType<ICampoConviteAcompanhanteRepository, CampoConviteAcompanhanteRepository<SCContext>>();
            //container.RegisterType<IEventoRepository, EventoRepository<SCContext>>();
            //container.RegisterType<IEventoOcorrenciaRepository, EventoOcorrenciaRepository<SCContext>>();
            //container.RegisterType<IItemListaRepository, ItemListaRepository<SCContext>>();
            //container.RegisterType<IListaGrupoRepository, ListaGrupoRepository<SCContext>>();
            //container.RegisterType<IMenuRepository, MenuRepository<SCContext>>();
            //container.RegisterType<IPerfilRepository, PerfilRepository<SCContext>>();
            //container.RegisterType<IConviteRepository, ConviteRepository<SCContext>>();
            //container.RegisterType<ISubCotaConviteRepository, SubCotaConviteRepository<SCContext>>();
            //container.RegisterType<ISubCotaConviteOcorrenciaRepository, SubCotaConviteOcorrenciaRepository<SCContext>>();
            //container.RegisterType<ISubEventoCampoConviteRepository, SubEventoCampoConviteRepository<SCContext>>();
            //container.RegisterType<ISubEventoCampoConviteAcompanhanteRepository, SubEventoCampoConviteAcompanhanteRepository<SCContext>>();
            //container.RegisterType<ISubEventoRepository, SubEventoRepository<SCContext>>();
            //container.RegisterType<ISubEventoLayoutEmailRepository, SubEventoLayoutEmailRepository<SCContext>>();
            //container.RegisterType<ISubEventoLayoutEmailOcorrenciaRepository, SubEventoLayoutEmailOcorrenciaRepository<SCContext>>();
            //container.RegisterType<ISubEventoOcorrenciaRepository, SubEventoOcorrenciaRepository<SCContext>>();
            //container.RegisterType<IUsuarioRepository, UsuarioRepository<SCContext>>();
            //container.RegisterType<IUsuarioAcessoRepository, UsuarioAcessoRepository<SCContext>>();
            //container.RegisterType<IUsuarioMenuRepository, UsuarioMenuRepository<SCContext>>();

            //container.RegisterType<IStageLoteConvidadoRepository, StageLoteConvidadoRepository<SCContext>>();
            //container.RegisterType<IStageLoteConvidadoGeralRepository, StageLoteConvidadoGeralRepository<SCContext>>();
            //container.RegisterType<IStageConvidadoRepository, StageConvidadoRepository<SCContext>>();
            //container.RegisterType<IStageConvidadoCampoRepository, StageConvidadoCampoRepository<SCContext>>();
            //container.RegisterType<IStageConvidadoCampoIncidenteRepository, StageConvidadoCampoErroRepository<SCContext>>();
            //container.RegisterType<IStageAcompanhanteRepository, StageAcompanhanteRepository<SCContext>>();
            //container.RegisterType<IStageAcompanhanteCampoRepository, StageAcompanhanteCampoRepository<SCContext>>();
            //container.RegisterType<IStageAcompanhanteCampoIncidenteRepository, StageAcompanhanteCampoIncidenteRepository<SCContext>>();
            //container.RegisterType<IStageCredenciamentoRepository, StageCredenciamentoRepository<SCContext>>();
            //container.RegisterType<IStageCredenciamentoOcorrenciaRepository, StageCredenciamentoOcorrenciaRepository<SCContext>>();

            //container.RegisterType<IStageIncidenteRepository, StageIncidenteRepository<SCContext>>();


            //container.RegisterType<IImportacaoCredenciamentoRepository, ImportacaoCredenciamentoRepository<SCContext>>();

            //container.RegisterType<ISubEventoDisparoEmailConviteRepository, SubEventoDisparoEmailConviteRepository<SCContext>>();
            //container.RegisterType<ISubEventoDisparoEmailConviteRetornoRepository, SubEventoDisparoEmailConviteRetornoRepository<SCContext>>();
            //container.RegisterType<ISubEventoDisparoEmailRepository, SubEventoDisparoEmailRepository<SCContext>>();
            //container.RegisterType<ISubEventoDisparoEmailOcorrenciaRepository, SubEventoDisparoEmailOcorrenciaRepository<SCContext>>();

            //container.RegisterType<ISubEventoLayoutEmailOcorrenciaRepository, SubEventoLayoutEmailOcorrenciaRepository<SCContext>>();

            //container.RegisterType<ISubEventoCheckinConviteOcorrenciaRepository, SubEventoCheckinConviteOcorrenciaRepository<SCContext>>();
            //container.RegisterType<ISubEventoCheckinConviteRepository, SubEventoCheckinConviteRepository<SCContext>>();
            //container.RegisterType<ISubEventoCheckinRepository, SubEventoCheckinRepository<SCContext>>();
            //container.RegisterType<ISubEventoUsuarioCredenciamentoRepository, SubEventoUsuarioCredenciamentoRepository<SCContext>>();
            //container.RegisterType<ISubEventoPreviewGeralRepository, SubEventoPreviewGeralRepository<SCContext>>();
            //container.RegisterType<IMensagemRepository, MensagemRepository<SCContext>>();

            //Serviços
            container.RegisterType<IMotoristasServices, MotoristasServices>();
            container.RegisterType<ICarrosServices, CarrosServices>();

            //container.RegisterType<IUsuariosService, UsuariosService>();
            //container.RegisterType<ICupomService, CupomService>();
            //container.RegisterType<INumerosDaSorteService, NumerosDaSorteService>();
            //container.RegisterType<IPremioInstantaneoService, PremioInstantaneoService>();
            //container.RegisterType<IRelatorioService, RelatorioService>();

            //container.RegisterType<IAcompanhanteCampoConviteAcompanhanteConteudoService, AcompanhanteCampoConviteAcompanhanteConteudoService>();
            //container.RegisterType<IAcompanhanteOcorrenciaService, AcompanhanteOcorrenciaService>();
            //container.RegisterType<IAcompanhanteOcorrenciaCamposService, AcompanhanteOcorrenciaCamposService>();
            //container.RegisterType<IAplicacaoService, AplicacaoService>();
            //container.RegisterType<IAreaService, AreaService>();
            //container.RegisterType<IClaimService, ClaimService>();
            //container.RegisterType<ICampoConviteService, CampoConviteService>();
            //container.RegisterType<ICampoConviteAcompanhanteService, CampoConviteAcompanhanteService>();
            //container.RegisterType<IConviteService, ConviteService>();
            //container.RegisterType<IConviteConfirmacaoDadosService, ConviteConfirmacaoDadosService>();
            //container.RegisterType<IConviteGeralService, ConviteGeralService>();
            //container.RegisterType<IConviteConvidadoService, ConviteConvidadoService>();
            //container.RegisterType<IConviteConvidadoReportService, ConviteConvidadoReportService>();
            //container.RegisterType<IConviteConvidadosGeralService, ConviteConvidadosGeralService>();
            //container.RegisterType<IConviteOcorrenciaService, ConviteOcorrenciaService>();
            //container.RegisterType<IConviteTotalDistribuidoService, ConviteTotalDistribuidoService>();
            //container.RegisterType<IConvidadoService, ConvidadoService>();
            //container.RegisterType<IConvidadoCampoConviteConteudoService, ConvidadoCampoConviteConteudoService>();
            //container.RegisterType<ICotaConviteService, CotaConviteService>();
            //container.RegisterType<ICotaConviteOcorrenciaService, CotaConviteOcorrenciaService>();
            //container.RegisterType<ICotaGeralService, CotaGeralService>();
            //container.RegisterType<IEventoService, EventoService>();
            //container.RegisterType<IEventoOcorrenciaService, EventoOcorrenciaService>();
            //container.RegisterType<IItemListaService, ItemListaService>();
            //container.RegisterType<IListaGrupoService, ListaGrupoService>();
            //container.RegisterType<IMenuService, MenuService>();
            //container.RegisterType<IPerfilService, PerfilService>();
            //container.RegisterType<ISubCotaConviteService, SubCotaConviteService>();
            //container.RegisterType<ISubCotaConviteOcorrenciaService, SubCotaConviteOcorrenciaService>();
            //container.RegisterType<ISubEventoCampoConviteService, SubEventoCampoConviteService>();
            //container.RegisterType<ISubEventoCampoConviteAcompanhanteService, SubEventoCampoConviteAcompanhanteService>();
            //container.RegisterType<ISubEventoService, SubEventoService>();
            //container.RegisterType<ISubEventoOcorrenciaService, SubEventoOcorrenciaService>();
            //container.RegisterType<ISubEventoLayoutEmailService, SubEventoLayoutEmailService>();
            //container.RegisterType<ISubEventoLayoutEmailOcorrenciaService, SubEventoLayoutEmailOcorrenciaService>();
            //container.RegisterType<IUsuarioService, UsuarioService>();
            //container.RegisterType<IUsuarioAcessoService, UsuarioAcessoService>();
            //container.RegisterType<IUsuarioMenuService, UsuarioMenuService>();

            //container.RegisterType<IStageLoteConvidadoService, StageLoteConvidadoService>();
            //container.RegisterType<IStageLoteConvidadoGeralService, StageLoteConvidadoGeralService>();
            //container.RegisterType<IStageConvidadoService, StageConvidadoService>();
            //container.RegisterType<IStageConvidadoCampoService, StageConvidadoCampoService>();
            //container.RegisterType<IStageConvidadoCampoIncidenteService, StageConvidadoCampoIncidenteService>();
            //container.RegisterType<IStageAcompanhanteService, StageAcompanhanteService>();
            //container.RegisterType<IStageAcompanhanteCampoService, StageAcompanhanteCampoService>();
            //container.RegisterType<IStageAcompanhanteCampoIncidenteService, StageAcompanhanteCampoIncidenteService>();
            //container.RegisterType<IStageCredenciamentoService, StageCredenciamentoService>();

            //container.RegisterType<IStageIncidenteService, StageIncidenteService>();

            //container.RegisterType<ISubEventoDisparoEmailConviteService, SubEventoDisparoEmailConviteService>();
            //container.RegisterType<ISubEventoDisparoEmailConviteRetornoService, SubEventoDisparoEmailConviteRetornoService>();
            //container.RegisterType<ISubEventoDisparoEmailService, SubEventoDisparoEmailService>();
            //container.RegisterType<ISubEventoDisparoEmailOcorrenciaService, SubEventoDisparoEmailOcorrenciaService>();

            //container.RegisterType<IDisparoEmailService, DisparoEmailService>();
            //container.RegisterType<IDisparoEmailProgramadoService, DisparoEmailProgramadoService>();
            //container.RegisterType<IRestricaoDisparoService, RestricaoDisparoService>();
            //container.RegisterType<IDisparoEmailAcaoService, DisparoEmailAcaoService>();
            //container.RegisterType<IRestricaoDisparoAcaoService, RestricaoDisparoAcaoService>();

            //container.RegisterType<ISubEventoCheckinConviteOcorrenciaService, SubEventoCheckinConviteOcorrenciaService>();
            //container.RegisterType<ISubEventoCheckinConviteService, SubEventoCheckinConviteService>();
            //container.RegisterType<ISubEventoCheckinService, SubEventoCheckinService>();

            //container.RegisterType<ISubEventoPreviewGeralService, SubEventoPreviewGeralService>();
            //container.RegisterType<IMensagemService, MensagemService>();

            ////Component
            //container.RegisterType<ISendEmailService, Group.SistemaDeConvites.Infra.Component.SparkPost.DisparoEmail.SendEmailService>();


        }

        public void Dispose()
        {
            _container.Value.Dispose();
        }
    }
}
