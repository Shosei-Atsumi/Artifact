class Video < ApplicationRecord
    belongs_to :channels, foreign_key: 'channel_id'
    has_many :VideoInfo, dependent: :destroy, primary_key: 'video_id'
end
