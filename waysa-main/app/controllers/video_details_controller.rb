class VideoDetailsController < ApplicationController
    before_action :authenticate_user!

    def show
        @videoInfos = VideoInfo
            .select(
                'MAX(created_at) AS created_at, 
                video_infos.video_title, 
                MAX(video_infos.video_view) AS video_view')
            .where(video_id: params[:video_id])
            .group('DAY(created_at)')
            .order(serial: "DESC")
        
        @channelInfo = ChannelInfo
            .select("channel_infos.channel_name")
            .find_by(channel_id: params[:channel_id])

        redirect_to show_path if @channelInfo.nil?

        @videoInfo = @videoInfos[0]

        redirect_to video_list_show_path(channel_id: params[:channel_id]) if @videoInfo.nil?
    end
end
