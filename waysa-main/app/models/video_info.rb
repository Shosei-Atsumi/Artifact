class VideoInfo < ApplicationRecord
    belongs_to :Video, foreign_key: 'video_id'
end
