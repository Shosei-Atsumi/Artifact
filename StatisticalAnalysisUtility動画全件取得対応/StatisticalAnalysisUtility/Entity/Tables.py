import DbUtility

class t_channel_info:
    def __init__(self, dbUtility):
        self.dbUtility = dbUtility
        self.channel_id = None
        self.serial = None
        self.channel_name = None
        self.channel_subscribers = None
        self.channnel_video_count = None
        self.channel_video_view_count = None

    def GetNextSerial(self, channel_id = ""):
        if(channel_id == ""):
            channel_id = self.channel_id

        maxSerial = self.dbUtility.SelectOne('t_channel_info','MAX(serial)' , self.dbUtility.AppendSql('channel_id =', self.dbUtility.ConvertToDbStr(channel_id)))
        if(maxSerial == None):
            maxSerial = 0

        maxSerial += 1
        return maxSerial

    def Insert(self):
        values = []
        values.append(self.dbUtility.ConvertToDbStr(self.channel_id))
        values.append(self.dbUtility.ConvertToDbStr(str(self.serial)))
        values.append(self.dbUtility.ConvertToDbStr(self.channel_name))
        values.append(self.dbUtility.ConvertToDbStr(str(self.channel_subscribers)))
        values.append(self.dbUtility.ConvertToDbStr(str(self.channnel_video_count)))
        values.append(self.dbUtility.ConvertToDbStr(str(self.channel_video_view_count)))
        self.dbUtility.Insert('t_channel_info', ['channel_id', 'serial', 'channel_name', 'channel_subscribers', 'channnel_video_count', 'channel_video_view_count'], values)

    def Clear(self):
       self.channel_id = None
       self.serial = None
       self.channel_name = None
       self.channel_subscribers = None
       self.channnel_video_count = None

class t_video:
    def __init__(self, dbUtility):
        self.dbUtility = dbUtility
        self.video_id = None
        self.video_title = None
        self.dt_video_upload = None
        self.channel_id = None

    def Insert(self):
        values = []
        values.append(self.dbUtility.ConvertToDbStr(self.video_id))
        values.append(self.dbUtility.ConvertToDbStr(self.video_title))
        values.append(self.dbUtility.ConvertToDbStr(self.dt_video_upload))
        values.append(self.dbUtility.ConvertToDbStr(self.channel_id))
        self.dbUtility.Insert('t_video', ['video_id', 'video_title', 'dt_video_upload', 'channel_id'], values)

    def Clear(self):
        self.video_id = None
        self.video_title = None
        self.dt_video_upload = None
        self.channel_id = None

class t_channel:
    def __init__(self, dbUtility):
        self.dbUtility = dbUtility
        self.channel_id = None

    def Insert(self):
        values = []
        values.append(self.dbUtility.ConvertToDbStr(self.channel_id))

        self.dbUtility.Insert('t_video', ['channel_id'], values)

    def Clear(self):
        self.channel_id = None