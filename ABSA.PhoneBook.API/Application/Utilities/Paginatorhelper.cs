using System;
namespace ABSA.PhoneBook.API.Application.Utilities
{
    public class Paginatorhelper
    {
        public static int? NextPage(int total,int page, int pageSize)
        {
            var lastPage = Math.Ceiling((decimal)total / pageSize);

            return page >= lastPage ? default(int?) : ++page;
        }

        public static int? PreviousPage(int total,int page)
        {
            return page <= 1 ? default(int?) : --page;
        }
    }
}
