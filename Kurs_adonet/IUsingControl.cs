using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs_adonet
{
    public enum CurrentControl
    {
        RegistrateControl,FilmFinderControl,LoginControl
    }
    public interface IUsingControl
    {
       CurrentControl ThisControl {  get; }
    }
}
