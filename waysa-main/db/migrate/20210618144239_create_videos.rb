class CreateVideos < ActiveRecord::Migration[5.2]
  def change
    create_table :videos do |t|
      t.string :video_id, :null => false
      t.string :video_title
      t.datetime  :video_upload_at
      t.string :channel_id
      t.timestamps
    end
  end
end
