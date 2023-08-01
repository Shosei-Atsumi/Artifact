class CreateChannelInfos < ActiveRecord::Migration[5.2]
  def change
    create_table :channel_infos do |t|
      t.string :channel_id, :null => false
      t.integer :serial, :null => false
      t.string :channel_name
      t.integer :channel_subscribers
      t.integer :channnel_video_count

      t.timestamps
    end
  end
end
