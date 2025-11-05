using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IBookRepositories
    {
        List<BookSummaryInfoDTO> GetNewBooksSummaryInfoList(int numberOfBook);
    }
}
