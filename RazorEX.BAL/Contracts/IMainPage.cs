using RazorEX.BAL.DTOs.MainPageDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Contracts
{
    public interface IMainPage
    {
        PageDTO GetData();
    }

}
