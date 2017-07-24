using Reparaciones_Julio.src.Situaciones;
using System.Xml.Serialization;

namespace Reparaciones_Julio.src.Interfaces
{
	[XmlInclude(typeof(SituacionCompleto))]
	[XmlInclude(typeof(SituacionIncompleto))]
	[XmlInclude(typeof(SituacionEnEspera))]
	[XmlInclude(typeof(SituacionVencido))]
	public abstract class Situacion
	{

		public abstract bool estaCompleto();
		public abstract bool estaEnEspera();
		public abstract bool estaVencido();

	}
}
