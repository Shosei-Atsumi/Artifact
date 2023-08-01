class ChannelInfo < ApplicationRecord
    belongs_to :channels, foreign_key: 'channel_id'
end
