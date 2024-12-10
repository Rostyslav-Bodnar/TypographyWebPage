using System.Security.Cryptography;

namespace Installer
{
    class Program
    {
        static void Main(string[] args)
        {
            KeyGenerator();
            Console.WriteLine("=== MyApp Installer ===");
            try
            {
                // Введення шляху для інсталяції
                Console.Write("Enter installation path (default: C:\\Program Files\\MyApp): ");
                string inputPath = Console.ReadLine();
                string targetPath = string.IsNullOrWhiteSpace(inputPath)
                    ? @"C:\Program Files\MyApp"
                    : inputPath;

                // Перевірка системних вимог
                if (!VerifySystemRequirements())
                {
                    Console.WriteLine("System requirements not met. Installation aborted.");
                    Console.ReadKey();
                    return;
                }

                // Створення директорії для інсталяції
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }

                // Копіювання файлів
                string appFolder = @"C:\Users\User\Desktop\Я\ДЗ\3 курс\1 семестр\Безпека програм та даних\Лабораторна робота №12\Lab12\bin\Release\net8.0-windows"; // Каталог із файлами програми
                if (!Directory.Exists(appFolder))
                {
                    Console.WriteLine($"Application folder '{appFolder}' not found. Installation aborted.");
                    Console.ReadKey();
                    return;
                }

                CopyDirectory(appFolder, targetPath);

                // Генерація ліцензійного ключа
                string systemKey = LicenseManager.GenerateSystemKey();
                if (systemKey == "UNKNOWN")
                {
                    Console.WriteLine("Failed to retrieve system information. Installation aborted.");
                    Console.ReadKey();
                    return;
                }

                string privateKeyPath = "privateKey.xml";
                if (!File.Exists(privateKeyPath))
                {
                    Console.WriteLine($"Private key file '{privateKeyPath}' not found. Installation aborted.");
                    Console.ReadKey();
                    return;
                }

                string privateKey = File.ReadAllText(privateKeyPath);
                string licenseKey = LicenseManager.GenerateLicenseKey(systemKey, privateKey);
                File.WriteAllText(Path.Combine(targetPath, "license.key"), licenseKey);

                Console.WriteLine("Installation completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during installation: {ex.Message}");
            }

            // Очікування натискання клавіші перед завершенням
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static bool VerifySystemRequirements()
        {
            // Перевірка системних вимог, наприклад, обсяг вільного місця
            long requiredDiskSpace = 50 * 1024 * 1024; // 50 MB
            string drive = Path.GetPathRoot(Environment.SystemDirectory);

            DriveInfo driveInfo = new DriveInfo(drive);
            if (driveInfo.AvailableFreeSpace < requiredDiskSpace)
            {
                Console.WriteLine("Insufficient disk space. Installation requires at least 50 MB.");
                return false;
            }

            // Інші перевірки, якщо необхідно
            return true;
        }

        private static void KeyGenerator()
        {
            using (RSA rsa = RSA.Create())
            {
                string privateKey = rsa.ToXmlString(true);
                File.WriteAllText("privateKey.xml", privateKey);
                Console.WriteLine("Private key saved to privateKey.xml");
            }
        }

        private static void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);

            // Копіюємо файли
            foreach (string filePath in Directory.GetFiles(sourceDir))
            {
                string fileName = Path.GetFileName(filePath);
                string destFilePath = Path.Combine(destDir, fileName);
                File.Copy(filePath, destFilePath, true);
                Console.WriteLine($"Copied: {fileName}");
            }

            // Копіюємо підкаталоги
            foreach (string directoryPath in Directory.GetDirectories(sourceDir))
            {
                string directoryName = Path.GetFileName(directoryPath);
                string destDirectoryPath = Path.Combine(destDir, directoryName);
                CopyDirectory(directoryPath, destDirectoryPath);
            }
        }
    }
}
