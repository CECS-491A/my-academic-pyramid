</<template>
  <v-app id="friendList">
    <v-layout row>
      <v-flex xs12 sm6 offset-sm3>
        <v-card>
          <v-toolbar color="teal" dark>
            <v-toolbar-side-icon></v-toolbar-side-icon>
  
            <v-toolbar-title class="text-xs-center">Friend List</v-toolbar-title>
  
            <v-spacer></v-spacer>
  
            <v-btn
              fab
              small
              color="gray"
              bottom
              right
              absolute
              @click="addFriendDialog = !addFriendDialog"
            >
              <v-icon>add</v-icon>
            </v-btn>
              <v-icon>search</v-icon>
            </v-btn>
          </v-toolbar>
  
          <v-list subheader>
     
            <v-list-tile
              v-for="friend in friendList"
              :key="friend.friendUsername"
              avatar
              @click="sendNewMessage(friend.FriendUsername)"
            >
              <!-- <v-list-tile-avatar>
                <img :src="item.avatar">
              </v-list-tile-avatar> -->
  
              <v-list-tile-content>
                <v-list-tile-title v-html="friend.FriendUsername"></v-list-tile-title>
              </v-list-tile-content>
  
              <!-- <v-list-tile-action>
                <v-icon :color="friend.active ? 'teal' : 'grey'">chat_bubble</v-icon>
              </v-list-tile-action> -->
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
      </v-flex>
    </v-layout>
  </v-app>
</template>

<script>
export default {
  data() {
    return {
      friendList: {
        FriendUserName: "",
        IsOnline: ""
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

    async addFriend(addFriendUsername) {
      await this.axios({
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

	sendNewMessage(receiverUsername)
	{
		this.$eventBus.$emit("SendMessageFromFriendList", receiverUsername)
		
	
	}
	

  }
};
</script>
