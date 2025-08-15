// 代码生成时间: 2025-08-15 14:50:02
public class BackupAndSyncTool
{
    // 定义数据库上下文
    private readonly MyDbContext _dbContext;

    // 构造函数
    public BackupAndSyncTool(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // 文件备份方法
    public void BackupFiles(string sourcePath, string backupPath)
    {
        try
        {
            // 检查源路径是否存在
            if (!Directory.Exists(sourcePath))
            {
                throw new DirectoryNotFoundException($"源路径 {sourcePath} 不存在。");
            }

            // 检查备份路径是否存在，如果不存在则创建
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
            }

            // 获取源路径中的所有文件
            var files = Directory.GetFiles(sourcePath);

            // 遍历文件并复制到备份路径
            foreach (var file in files)
            {
                var destFile = Path.Combine(backupPath, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            Console.WriteLine($"文件已成功备份到 {backupPath}。");
        }
        catch (Exception ex)
        {
            // 异常处理
            Console.WriteLine($"备份文件时发生错误：{ex.Message}");
        }
    }

    // 文件同步方法
    public void SyncFiles(string sourcePath, string targetPath)
    {
        try
        {
            // 检查源路径和目标路径是否存在
            if (!Directory.Exists(sourcePath) || !Directory.Exists(targetPath))
            {
                throw new DirectoryNotFoundException("源路径或目标路径不存在。");
            }

            // 获取源路径和目标路径中的所有文件
            var sourceFiles = Directory.GetFiles(sourcePath);
            var targetFiles = Directory.GetFiles(targetPath);

            // 遍历源路径中的文件，检查文件是否存在于目标路径
            foreach (var sourceFile in sourceFiles)
            {
                var fileName = Path.GetFileName(sourceFile);
                var targetFile = Path.Combine(targetPath, fileName);

                // 如果目标路径中不存在，则复制文件
                if (!File.Exists(targetFile))
                {
                    File.Copy(sourceFile, targetFile, true);
                }
            }

            // 遍历目标路径中的文件，检查文件是否存在于源路径
            foreach (var targetFile in targetFiles)
            {
                var fileName = Path.GetFileName(targetFile);
                var sourceFile = Path.Combine(sourcePath, fileName);

                // 如果源路径中不存在，则删除目标路径中的文件
                if (!File.Exists(sourceFile))
                {
                    File.Delete(targetFile);
                }
            }

            Console.WriteLine($"文件已成功同步到 {targetPath}。");
        }
        catch (Exception ex)
        {
            // 异常处理
            Console.WriteLine($"同步文件时发生错误：{ex.Message}");
        }
    }
}

// 数据库上下文类
public class MyDbContext : DbContext
{
    public DbSet<FileRecord> FileRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("YourConnectionString");
    }
}

// 文件记录实体类
public class FileRecord
{
    public int Id { get; set; }
    public string FilePath { get; set; }
    public DateTime LastModifiedTime { get; set; }
}
