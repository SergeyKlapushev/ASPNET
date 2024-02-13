using sem1.Dto;

namespace sem1.Abstraction
{
    public interface IStorageService
    {
        IEnumerable<StorageDto> GetStorage();
        int AddStorage(StorageDto product);
    }
}
