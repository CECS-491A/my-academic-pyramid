</<template>
  <v-app id="friendList"> 
        <v-card>
          <v-toolbar color="teal" dark>
            <v-spacer></v-spacer>
            <v-btn
              fab
              small
              color="red"
              top
              right
              absolute
              @click="addFriendDialog = !addFriendDialog"
            >  
            <v-icon>add</v-icon>
            </v-btn>
              
      
          </v-toolbar>
  
          <v-list subheader>
            <v-list-tile
              v-for="friend in friendList"
              :key="friend.FriendId"
              >
              <v-list-tile-content
              @click="sendNewMessage(friend.FriendId, friend.FriendUsername)">
                <v-list-tile-title> {{friend.FriendUsername}}
                </v-list-tile-title>
              </v-list-tile-content>
                     <v-list-tile-action>
              </v-list-tile-action>
               <v-menu bottom left>
              <template v-slot:activator="{ on }">
                <v-btn
                  dark
                  icon
                  v-on="on"
                  color="black"
                  
                >
                  <v-icon>more_vert</v-icon>
                </v-btn>
              </template>
               <v-card>
          <v-list>
            <v-list-tile avatar>
              <v-list-tile-avatar>
                <img src="https://cdn.vuetifyjs.com/images/john.jpg" alt="John">
              </v-list-tile-avatar>
  
              <v-list-tile-content>
                <v-list-tile-title>{{friend.FriendUsername}}</v-list-tile-title>
                <v-list-tile-sub-title></v-list-tile-sub-title>
              </v-list-tile-content>
  
              <v-list-tile-action>
                <v-btn
                  icon
                  @click="deleteFriend(friend.FriendId)"
                >
                  <v-icon>delete</v-icon>
                </v-btn>
              </v-list-tile-action>
            </v-list-tile>
          </v-list>
  
          <v-divider></v-divider>
  
         
  
          <v-card-actions>
            <v-spacer></v-spacer>
  
            <v-btn flat @click="menu = false">Cancel</v-btn>
            <v-btn color="primary" flat @click="menu = false">Save</v-btn>
          </v-card-actions>
        </v-card>
              
  
            
            </v-menu>
            </v-list-tile>
          </v-list>
          <v-divider></v-divider>
  
        </v-card>

		<v-dialog v-model="addFriendDialog" max-width="500px">
            <v-card>
              <v-card-text>
                <v-text-field 
                label="Friend'username"
                v-model="addFriendUsername"
                ></v-text-field>
               
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn flat color="primary" @click="addFriend">Add</v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
  </v-app>
</template>

<script>
export default {
  data() {
    return {
      friendList: {
        FriendId: "",
        FriendUserName: "",
        IsOnline: false
      },

      friendTO:{
        Id:"",
        username:"",

      },

      addFriendUsername:"",
      addFriendDialog: false
    };
  },

  created() {
    this.loadFriendList();
  },
  methods: {
    async loadFriendList() {
      await this.axios({
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.SITtoken
        },
        method: "GET",
        crossDomain: true,
        url: this.$hostname + "messenger/GetFriendList"
      })
        .then(response => {
          this.friendList = response.data;
        })
        .catch(err => {
          /* eslint no-console: "off" */
          console.log(err);
        });
    },

    async addFriend() {
      await this.axios({
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.SITtoken
        },
        method: "POST",
        crossDomain: true,
        url: this.$hostname + "messenger/AddFriend?addedUsername=" + this.addFriendUsername
      })
        .then(response => {
			this.addFriendDialog = false,
			this.loadFriendList()

        //this.friendList.push({FriendUserName: this.addFriendUsername});
        })
        .catch(err => {
          /* eslint no-console: "off" */
          console.log(err);
        });
	},

	sendNewMessage(friendId,friendUsername)
	{
    this.friendTO.Id = friendId,
    this.friendTO.username = friendUsername
		this.$eventBus.$emit("SendMessageFromFriendList", this.friendTO)
		
	
  },
  
  async deleteFriend(friendId){
    await this.axios({
      headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.SITtoken
        },
        method: "DELETE",
        crossDomain: true,
        url: this.$hostname + "messenger/RemoveFriendFromList?friendId=" + friendId
      })
        .then(response => {
			this.loadFriendList()

        //this.friendList.push({FriendUserName: this.addFriendUsername});
        })
        .catch(err => {
          /* eslint no-console: "off" */
          console.log(err);
        });
  }
	

  }
};
</script>
