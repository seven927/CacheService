# CacheService
This is an excise to implement a cache with expiration time. 


## Install
Install [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/). This will install .Net Core SDK as well. When you are asked to select workload, select ASP.NET and web development. Refer to this [guide](https://learn.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2022) for more details.

![image](https://user-images.githubusercontent.com/7350037/203203056-61039346-f42f-4271-85ac-e4514a682c27.png)

## Test

Run Visual Studio from source and in the launched SwaggerUI, use POST endpoint to add a user. Then use the GET endpoint to get the user. Look at debug windown in Visual Studio. You should see "Get user profile from storage" since there is no such item in the cache. Request this user again and you should see "Get user profile from cache" this time. After 5 minutes, call the GET endpoint again and you should see "Get user profile from storage" again since the cached item has been removed.  

<img width="598" alt="image" src="https://user-images.githubusercontent.com/7350037/205762399-4c28c7f9-e8cb-4b61-96da-ca4db51b320e.png">

## To Do
Currently, this cache does not have a size limit. We can add a size limit and implement LRU to evict least recently used cache entry when size hits the limit.
