using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Proud
{
    class FileSync
    {
        static public System.String GetCDNFile(System.String cdnUrl , System.String saveFilePath)
        {
            if (File.Exists(saveFilePath))
            {
                System.String remoteFileHash = CalculateRemoteFileHash(cdnUrl);
                System.String localFileHash = CalculateFileHash(saveFilePath);

                if (remoteFileHash != localFileHash)
                {
                    Console.WriteLine("파일이 변경되었습니다. 다운로드합니다.");

                    // 파일을 다운로드합니다.
                    DownloadFile(cdnUrl, saveFilePath);
                }
                else
                {
                    Console.WriteLine("파일은 변경되지 않았습니다.");
                }
            }
            else
            {
                Console.WriteLine("로컬에 파일이 없습니다. 파일을 다운로드합니다.");

                // 파일을 다운로드합니다.
                DownloadFile(cdnUrl, saveFilePath);
            }

            return GetFileText(saveFilePath);
        }

        static System.String CalculateFileHash(System.String filePath)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hashBytes = md5.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }
        }

        static System.String CalculateRemoteFileHash(System.String fileUrl)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    byte[] fileData = client.DownloadData(fileUrl);
                    using (var md5 = System.Security.Cryptography.MD5.Create())
                    {
                        byte[] hashBytes = md5.ComputeHash(fileData);
                        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("원격 파일의 해시를 계산하는 중 오류 발생: " + ex.Message);
                return null;
            }
        }

        static void DownloadFile(System.String fileUrl, System.String savePath)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(fileUrl, savePath);
                    Console.WriteLine("파일 다운로드 완료: " + savePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("파일 다운로드 중 오류 발생: " + ex.Message);
            }
        }

        static System.String GetFileText(System.String filePath)
        {
            try
            {
                // 파일 내용을 읽어올 StreamReader를 생성합니다.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // 파일 내용을 문자열로 읽어옵니다.
                    System.String fileContent = sr.ReadToEnd();
                    if (IsCsvString(fileContent) is false)
                        return null;

                    return fileContent;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"파일을 읽을 수 없습니다: {e.Message}");
            }

            return null;
        }

        static bool IsCsvString(string input)
        {
            string[] records = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string record in records)
            {
                string[] fields = record.Split(',');

                foreach (string field in fields)
                {
                    if (Regex.Matches(field, "\"").Count % 2 != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}