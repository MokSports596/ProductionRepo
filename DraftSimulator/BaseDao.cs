using Mok.Web.Data.Dto;

namespace Mok.Web.Data.Dao
{
    public interface BaseDao
    {
        bool Insert(BaseDto dto);
        bool Read(BaseDto dto);
        bool Update(BaseDto dto);
        bool Delete(BaseDto dto);
    }
}
