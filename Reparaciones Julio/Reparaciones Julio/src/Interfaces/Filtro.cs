﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reparaciones_Julio.src.Interfaces
{
	public interface Filtro
	{
		List<Cliente> aplicar(List<Cliente> clientes);
	}
}
