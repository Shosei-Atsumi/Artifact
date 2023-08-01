class CreateProfiles < ActiveRecord::Migration[5.2]
  def change
    create_table :Profiles, primary_key: :user_id do |t|
      t.string :name, :null => false
      t.string :comment

      t.timestamps
    end
  end
end
