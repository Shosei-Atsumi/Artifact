class CreateVideoInfos < ActiveRecord::Migration[5.2]
  def change
    create_table :video_infos do |t|
      t.string :video_id, :null => false
      t.integer :serial, :null => false
      t.string :video_title
      t.integer :video_view
      t.timestamps
    end
  end
end
