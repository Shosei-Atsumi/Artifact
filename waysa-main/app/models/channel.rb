class Channel < ApplicationRecord
    has_many :ChannelInfo, dependent: :destroy, primary_key: 'channel_id'
    has_many :Video, dependent: :destroy
end
