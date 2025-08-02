using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistorInterpretor.Contracts
{
    public interface IRadioButtonManager
    {
        event EventHandler? BandCountChanged;
        int GetValue();
        int UpdateBandCount();
        void SetBandCount(int bandCount);

    }
}
