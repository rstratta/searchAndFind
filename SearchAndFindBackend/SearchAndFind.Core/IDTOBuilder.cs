using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public interface IDTOBuilder<D,E>
    {
        D BuildDTO(E entity);
    }
}
