#include <bits/stdc++.h>
using namespace std;
#define P pair<int, int>
#define fi first
#define se second
#include <fstream>

vector<int> rG[10005]; //反向边
int sz[10005];         //可拓展状态数
int a[2], b[2];
P tmp;
int res[10005]; // 1代表必胜, -1必败, 0未定
int vis[10005]; // bfs用
queue<int> que, que1, que2;
vector<int> vec1, vec2;
int cnt1, cnt2;

int main()
{
    que.push(1111);
    vis[1111] = 1;
    //	不是所有的四位数都能达到（自己觉得）
    //	所以用bfs遍历而不用 for i in [100, 9999]
    while (!que.empty())
    {
        int i = que.front();
        que.pop();
        //	i 的千分位和百分位代表当前行动者的数字, 十分位和个位代表对手
        //	并保证前者不大于后者, 即已去重
        a[0] = i % 10;
        a[1] = i / 10 % 10;
        b[0] = i / 100 % 10;
        b[1] = i / 1000;
        vector<P> p;
        for (int j = 0; j < 4; j++)
        {
            if (b[j >> 1] == 0 || a[j & 1] == 0)
                continue;
            tmp = {(b[j >> 1] + a[j & 1]) % 10, b[1 - (j >> 1)]};
            if (tmp.fi > tmp.se)
                swap(tmp.fi, tmp.se);
            p.push_back(tmp);
        }
        //	去重
        sort(p.begin(), p.end());
        p.erase(unique(p.begin(), p.end()), p.end());

        sz[i] = p.size();
        int res1 = a[1] * 1000 + a[0] * 100;
        for (int j = 0; j < p.size(); j++)
        {
            int res2 = p[j].fi * 10 + p[j].se;
            rG[res1 + res2].push_back(i);
            if (!vis[res1 + res2])
            {
                vis[res1 + res2] = 1;
                que.push(res1 + res2);
            }
        }
    }
    for (int i = 1; i <= 99; i++)
    {
        // i00实际不存在, 它的前驱是必胜态, 所以暂时放在必败队列里
        if (vis[i * 100])
            que2.push(i * 100);
    }
    for (; !que2.empty();)
    {
        //	必败向必胜转移
        while (!que2.empty())
        {
            int q = que2.front();
            que2.pop();
            for (int i = 0; i < rG[q].size(); i++)
            {
                int to = rG[q][i];
                if (!res[to])
                {
                    res[to] = 1;
                    que1.push(to);
                }
            }
        }
        //	必胜向必败转移
        while (!que1.empty())
        {
            int q = que1.front();
            que1.pop();
            for (int i = 0; i < rG[q].size(); i++)
            {
                int to = rG[q][i];
                if (!res[to])
                {
                    if (--sz[to] == 0)
                    {
                        res[to] = -1;
                        que2.push(to);
                    }
                    assert(sz[to] >= 0);
                }
            }
        }
    }
    for (int i = 0; i <= 9999; i++)
    {
        if (res[i] == 1)
        {
            vec1.push_back(i);
            cnt1++;
        }
        if (res[i] == -1)
        {
            vec2.push_back(i);
            cnt2++;
        }
    }

    //printf("必胜态共%d种:\n", cnt1);

    ofstream success_file("success.txt", ios::out);
    if (!success_file)
    {
        cout << "error opening success.txt" << endl;
        return 0;
    }
    for (int i : vec1)
    {
        if (i < 1000)
        {
            success_file << 0 << i << endl;
        }
        else
        {
            success_file << i << endl;
        }
    }
    success_file.close();

    //printf("必败态共%d种:\n", cnt2);
    ofstream fail_file("fail.txt", ios::out);
    if (!fail_file)
    {
        cout << "error opening fail.txt" << endl;
        return 0;
    }
    for (int i : vec2)
    {
        if (i < 1000)
        {
            fail_file << 0 << i << endl;
        }
        else
        {
            fail_file << i << endl;
        }
    }
    fail_file.close();
}
