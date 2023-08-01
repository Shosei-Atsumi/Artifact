import MySQLdb
import re
class DbUtility():

    conn = None
    cur = None

    def __init__(self, conn = None):
        if(conn == None):
            self.conn = MySQLdb.connect(
            unix_socket = '/Applications/MAMP/tmp/mysql/mysql.sock',
            user='su',
            passwd='99999999',
            host='localhost',
            db='statisticalanalysisdb',
            charset="utf8")
        else:
            self.conn = conn

        self.cur = self.conn.cursor()

    def __del__(self):
        self.cur.close
        self.conn.close

    def SelectAll(self, tableName, columnName = '', where = ''):
        sql = ''
        if(columnName):
            sql = self.AppendSql('SELECT', columnName)
        else:
            sql = self.AppendSql('SELECT', '*')

        sql = self.AppendSql(sql, 'FROM')
        sql = self.AppendSql(sql, tableName)
        
        self.cur.execute(sql)
        rows = self.cur.fetchall()
    
        return rows

    def SelectOne(self, tableName, columnName = '', where = ''):
        sql = ''
        if(columnName):
            sql = self.AppendSql('SELECT', columnName)
        else:
            sql = self.AppendSql('SELECT', '*')

        sql = self.AppendSql(sql, 'FROM')
        sql = self.AppendSql(sql, tableName)

        if(where):
            sql = self.AppendSql(sql, self.AppendSql('WHERE', where))
        
        self.cur.execute(sql)
        row = self.cur.fetchone()
    
        if(row != None):
            return row[0]
        else: 
            return None
        
    
    def Insert(self, tableName, columnNames, Values):
        sql = ''
        sql = self.AppendSql('INSERT INTO', tableName)
        if(columnNames != None):
            columnStr = ''
            for name in columnNames:
                columnStr = self.StrCombineWithCommas(columnStr, name)

            columnStr = self.StrDeleteEndCommas(columnStr)
            columnStr = self.StrAddParentheses(columnStr)

            sql = self.AppendSql(sql, columnStr)
        
        valueStr = ''
        for value in Values:
            valueStr = self.StrCombineWithCommas(valueStr, value)

        valueStr = self.StrDeleteEndCommas(valueStr)
        valueStr = self.StrAddParentheses(valueStr)
        sql = self.AppendSql(sql, 'VALUES')
        sql = self.AppendSql(sql, valueStr)

        self.Execute(sql)

    def Execute(self, sql):
        res = ""
        try:
            res = self.cur.execute(sql)
            self.conn.commit()
        except: 
            return res
        return res


    def AppendSql(self, sql, string):
         sql += ' ' + string
         return sql

    def StrCombineWithCommas(self, string, strSecond):
         string += strSecond + ','
         return string

    def StrDeleteEndCommas(self, string):
         string = re.sub(',$', '', string)
         return string

    def StrAddParentheses(self, string):
        string = '(' + string + ')'
        return string
   
    def ConvertToDbStr(self, value):
        if(type(value) is not str):
            value = str(value)

        value = re.sub('\'', '\\\'', value)
        string = "'" + value + "'"
        return string
        