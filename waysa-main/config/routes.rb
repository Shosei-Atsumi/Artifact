Rails.application.routes.draw do
  root 'top#index'

  devise_for :users
  get '/mypage', to: 'mypage#show', as: 'show'
  get '/mypage_edit', to: 'mypage#edit', as: 'edit'

  post '/mypage', to: 'mypage#update', as: 'update'
  get '/channel_details', to: 'channel_details#show', as: 'channel_details_show'
  get '/video_list', to: 'video_list#show', as: 'video_list_show'
  get '/video_details', to: 'video_details#show', as: 'video_details_show'
  
end
