class ChannelDetailsController < ApplicationController
    before_action :authenticate_user!
    
    def show
        @channelInfos = ChannelInfo
            .select(
                'MAX(created_at) AS created_at, 
                channel_infos.channel_name, 
                MAX(channel_infos.channel_subscribers) AS channel_subscribers, 
                MAX(channel_infos.channnel_video_count) AS channnel_video_count, 
                MAX(channel_infos.channel_video_view_count) AS channel_video_view_count')
            .where(channel_id: params[:channel_id])
            .group('DAY(created_at)')
            .order(serial: "DESC")

        @channelInfo = @channelInfos[0]
        redirect_to show_path if @channelInfo.nil?
    end
end
