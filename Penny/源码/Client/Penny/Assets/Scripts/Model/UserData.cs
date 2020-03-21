using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class UserData
{
    public  UserData(string data)
    {
        string[] strs = data.Split(',');
        this.UserId =int.Parse( strs[0]);
        this.UserName = strs[1];
        this.TotalCount = int.Parse(strs[2]);
        this.WinCount = int.Parse(strs[3]);

    }
    public UserData(string userName,int totalCount,int winCount)
    {
            this.UserName = userName;
            this.TotalCount = totalCount;
            this.WinCount = winCount;
    }
    public UserData(int userId, string userName, int totalCount, int winCount)
    {
        this.UserId = userId;
        this.UserName = userName;
        this.TotalCount = totalCount;
        this.WinCount = winCount;
    }
    public int UserId { get; set; }
    public string UserName { get;private set; }
    public int TotalCount { get;  set; }
    public int WinCount { get;  set; }
}
