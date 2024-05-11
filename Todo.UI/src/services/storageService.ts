export interface IStorageService {
  read(key: string): Promise<string>;
  save(key: string, value: string): Promise<void>;
  remove(key: string): Promise<void>;
}

class StorageService implements IStorageService {
  async read(key: string) {
    return new Promise<string>(() => localStorage.getItem(key));
  }

  async save(key: string, value: string) {
    localStorage.setItem(key, value);
  }

  async remove(key: string) {
    localStorage.removeItem(key);
  }
}

export default new StorageService();
