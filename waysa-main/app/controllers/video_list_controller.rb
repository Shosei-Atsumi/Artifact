class VideoListController < ApplicationController
    before_action :authenticate_user!

    def show
        @videos = Video.left_joins(:VideoInfo)
        .select("
            videos.video_id, 
            videos.video_title, 
            MAX(video_infos.serial), 
            MAX(video_infos.video_view) AS video_view, 
            MAX(videos.video_upload_at) AS video_upload_at, 
            MAX(video_infos.created_at) AS created_at"
        )
        .where(channel_id: params[:channel_id])
        .group(:video_id)
        .order(video_upload_at: "DESC")

        @channelInfo = ChannelInfo
        .select("channel_infos.channel_name")
        .find_by(channel_id: params[:channel_id])
        #logger.debug @channelInfos.inspect

        redirect_to show_path if @channelInfo.nil?
    end
end
