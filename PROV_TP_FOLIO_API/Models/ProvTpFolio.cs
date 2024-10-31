namespace PROV_TP_FOLIO_API.Models
{
    public class ProvTpFolio
    {
        public int FolioLlavPr { get; set; }
        public int TipoSolicitud { get; set; }
        public int Estatus { get; set; }
        public int? NumeroSolicitudes { get; set; }
        public int? NsAceptadas { get; set; }
        public int? NsRechazadas { get; set; }
        public int? Notificacion { get; set; }
        public string? UsuaNllavPr { get; set; }
        public string? UsuaMails { get; set; }
        public string? CecoLlavPr { get; set; }
        public int? NegoLlavPr { get; set; }
        public string? UsuaCllavPr { get; set; }
        public string? UsuaLlavPr { get; set; }
        public DateTime? TmpoLlavPr { get; set; }
        public int? NsAutorizadas { get; set; }

    }
}
