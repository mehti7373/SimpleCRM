using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class BaseDTO<TKey, TTitle>
    {
        public TKey Id { get; set; }

        public TTitle Title { get; set; }

        public BaseDTO()
        {
        }

        public BaseDTO(TKey key, TTitle title)
        {
            Id = key;
            Title = title;
        }
    }
}
