using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testxueji.Models;

namespace vuexueji.DAL
{
    interface IDataProvider
    {
         IEnumerable List(string temp);

    }
}
