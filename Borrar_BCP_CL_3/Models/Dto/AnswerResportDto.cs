namespace Borrar_BCP_CL_3.Models.Dto
{
    public class AnswerResportDto
    {
        public virtual ICollection<ReporteDto>? Reports { get; set; }
        public int? cuantity { get; set; }
    }
}
