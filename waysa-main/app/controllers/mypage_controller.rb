class MypageController < ApplicationController

    before_action :authenticate_user!

    def show
        # プロフィール取得
        @profile = Profile.find_by(user_id: current_user.id)
        # プロフィールが設定されていない場合、設定画面へ
        if @profile.nil? then
            render "mypage/edit"
        end

        # 登録チャンネル取得
        @channels = Channel.left_joins(:ChannelInfo)
        .select("
            channels.channel_id, 
            channel_infos.channel_name, 
            MAX(channel_infos.serial), 
            MAX(channel_infos.channel_subscribers) AS channel_subscribers, 
            MAX(channel_infos.channnel_video_count) AS channnel_video_count, 
            MAX(channel_infos.channel_video_view_count) AS channel_video_view_count")
            .group(:channel_id)

        #logger.debug profile.inspect
       
        render "mypage/mypage"
    end

    def edit
        @profile = Profile.find_by(user_id: current_user.id)
    end

    def update
        profile = Profile.find_by(user_id: current_user.id)

        #logger.debug params.inspect
        #logger.debug profile.inspect
        if profile.nil? then
            Profile.create(user_id: current_user.id, name: params[:name], comment: params[:comment])
        else
            profile.update(name: params[:name], comment: params[:comment])
            profile.save
        end
        redirect_to '/mypage'
    end
end
