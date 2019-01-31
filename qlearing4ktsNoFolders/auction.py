import numpy as np
from game import Game
import random
from matplotlib.pyplot import step

class Auction(Game):
    
        
    @property
    def name(self):
        return "Auction"
    
    """Returns: number of possible moves in a round: {bid, pass}"""
    @property
    def nb_actions(self):
        return 2
    
    
    
    q = None
    p = None
    
    
    userOutcome = None
    opponentOutcome = None
    
    
    userAverage = None
    opponentAverage = None
    
    EUser = None
    EOpponent = None
    
    
    UserARoundCount = None
    OpponentARoundCount = None
    
    
    UserERoundCount = None
    OpponentERoundCount = None
    
    
    isUserFirst = None
    
    
    gameOver = False
    isUserWon = False
    K = 1000
    step = 1
    maxMove = None
    
    def __init__(self,isFirst=False,N=100,maxMove=1000,opponentOutcome=None,K=2000, p=None, q=None, isUserFirst=True):
        #initiate global fields
        #self.testSimulGame()
        #return 
    
    
        K = maxMove
        self.EUser = np.zeros((K))
        self.EOpponent = np.zeros((K))
        self.userOutcome = np.zeros(K)
        self.UserARoundCount = np.zeros(K)
        self.OpponentARoundCount = np.zeros(K)
        self.UserERoundCount = np.zeros(K)
        self.UserARoundCount = np.zeros(K)
        
        
        self.userAverage = np.zeros(K)
        self.opponentAverage = np.zeros(K)
        self.isUserFirst = isUserFirst
        
        if not self.p:
            self.p = np.ones(maxMove)
        if not self.q:
            self.q = np.arange(0.0001,1,0.0001)
         
        self.calcualteBasicAlgorithm()
        print(self.userAverage)   
        #self.reset()
        
        
        
        
    def updateAverage(self,k0,userLastRound,K):
        for i in range(userLastRound,k0,2):
            self.userOutcome[i] = self.userOutcome[i] + (K-userLastRound)
            self.UserARoundCount[i] += 1
            
        for i in  range(0,k0-1,2):
            self.userOutcome[i] = self.userOutcome[i] - i
            self.UserARoundCount[i] += 1
            
        for i in range(0,k0,2):
            self.userAverage[i] = self.userOutcome[i] / self.UserARoundCount
            
            
    def updateDistributions(self,k0,listOfEndGames,K):
        distr = 0
        for i in listOfEndGames:
            distr = distr + (K-k0)*self.calcProbability(i)
            self.UserERoundCount += 1
        self.EUser = distr / self.UserERoundCount
        
        
    #TODO: check how to update backward distribution
        
        
    def calcProbability(self,pFinal):
        p = 1
        if self.isUserFirst :
            for i in range(0,pFinal):
                if i%2 == 0:
                    p = p*self.self.p[i]
                else:
                    p = p*self.q[i]
        return p
                
    
    
    def findKs(self,K,w=10):
        size = self.userAverage.size
        k0tag = 0
        
        for i in range(0,size):
            if k0tag < self.userAverage[i]:
                k0tag = self.userAverage[i]
        min=0
        minIndex = 0 
        for i in range(0,k0tag):
            if min > self.userAverage[i]:
                min = self.userAverage[i]
                minIndex = i
        
        min = min if (min > (-K/w)) else (-K/w)   
        return min , minIndex 
                
    
    def calcualteBasicAlgorithm(self,n0=10,m=3,K=1000,N=1000,y=10,z=5):
        '''first step'''
        for i in range(0,n0):
            endGame = False
            i = 0
            while not endGame:
                r = random.random()
                if r < self.p[i]:
                    i+=1
                    r = random.random()
                    if r < self.q[i]:
                        i+=1
                    else:
                        endGame = True
                        break
                else:
                    endaGame = True
                    break
            self.updateAverage(m*K, i, K)
            self.updateDistributions(m*K, i, K)
        
        
        '''second step'''
        for i in range(0,N/y):
            j = 2
            endGame = False
            i = 0
            while not endGame:
                r = random.random()
                if r < self.p[i]:
                    if (K - i) < -K:
                        endaGame = True
                        break
                    else:
                        i += 1
                        r = random.random()
                        if r < self.q[i]:
                            i+=1
                        else:
                            endGame = True
                            break
                else:
                    endaGame = True
                    break
                
            self.updateAverage(m*j*K, i, K)
            self.updateDistributions(m*j*K, i, K)
            j += 1
        
        
        
        '''Third and fourth steps'''
        for i in range(0,1+N/z):
            k0min , k0minIndex = self.findKs(K, w)
            if k0min != 0:
                for i in range(0,k0min):
                    endGame = False
                    i = 0
                    while not endGame:
                        r = random.random()
                        if r < self.p[i]:
                            i+=1
                            r = random.random()
                            if r < self.q[i]:
                                i+=1
                            else:
                                endGame = True
                                break
                        else:
                            endaGame = True
                            break
                    self.updateAverage(k0minIndex, i, K)
                    self.updateDistributions(k0minIndex, i, K)
        '''Fifth step'''
        max = 0
        maxIndex = 0
        for i in range(0,self.userAverage.size):
            if max > self.userAverage[i] :
                max = self.userAverage[i]
                maxIndex = i
        
        #Subtract from N the number of games played so far 
        rN = N-(1+N/z+N/y+n0)      
        if self.userAverage[maxIndex] >= 0:
            for i in  range(0,rn):
                for i in range(0,maxIndex):
                    endGame = False
                    i = 0
                    while not endGame:
                        r = random.random()
                        if r < self.p[i]:
                            i+=1
                            r = random.random()
                            if r < self.q[i]:
                                i+=1
                            else:
                                endGame = True
                                break
                        else:
                            endaGame = True
                            break
                    self.updateAverage(k0minIndex, i, K)
                    self.updateDistributions(k0minIndex, i, K)

    """Resets the game, TODO: Further explanation later-TBD :P """
    def reset(self):
        #default game setting
        self.calculateOutcome()
        #reset number of steps
        self.step = 1
        
    """Implementation of a single round of a game, a single round consists of two moves, the player's move and the opponent's response,
        the order of playing my vary. """    
    def play(self, action):
		#Decided to pass
        print("current action is {0}".format(action))
        if not action:
            self.passGame()
         #Check if we played maxMoves
        elif self.step > K:
             self.passGame()
        #if the player is still has a positive income, he will stay in the game
        elif  self.calculateExpecation() < (self.K - self.step):
            self.passGame()
        else:
            self.step+=1
            #emulate opponents move,
            #if 1 then the opponent is still playing
            #if 0 then the opponents passed and hence the player won
            opponentsMove=np.random.choice(1,1)
            print(opponentsMove[0])
            if not opponentsMove[0]:
                self.winGame()
            
            


    def get_state(self):
        return self.userOutcome

    def get_score(self):
        return (K-self.step)

    def is_over(self):
        return self.gameOver

    def is_won(self):
        return self.isUserWon

    def get_frame(self):
        return self.get_state()

    def draw(self):
        return self.get_state()

    def get_possible_actions(self):
        return range(self.nb_actions)
    
    
    
    
    
    
    def passGame(self):
        self.isUserWon = False
        self.gameOver = True
        return
    
    def winGame(self):
        self.isUserWon = True
        self.gameOver = True
        return
    
    """ Calculates the possible expectation from the auction so far,
        Since the opponents possible move is considered a hidden information
        we do not include its calculation, or even consider it in our decision making at this
        phase.
        Returns:
            Expectations[step]. """
    def calculateExpecation(self):
        #TODO: to write the loss expectation
        self.EUser[0,self.step] = ( self.userOutcome[0,self.step]*self.p[self.step] + (-self.step)*self.p[self.step] ) * ( 1 if self.step==1 else self.EUser[0,self.step-2] )
        return self.EUser[0,self.step]
        
    
    
    def calculateOutcome(self,isFirst=False,N=100,maxMove=1000,opponentOutcome=None,K=2000):
        
        self.userOutcome = np.zeros((2,maxMove))
        
        self.q = np.arange(0.0001,1,0.0001)
        self.p = np.zeros(maxMove)
        
        #toggling the turn of the user
        #based on the input parameter: boolean isFirst
        usersTurn = isFirst^1
        
        if self.opponentOutcome is None:
            self.opponentOutcome = np.zeros((2,maxMove))
            #print(self.opponentOutcome.size)
        for i in range(0,N):
            for iMove in range(0,maxMove):
                if(iMove%2 == usersTurn):
                    usersMove = np.random.uniform(0.0,1.0)
                    if(usersMove>self.q[iMove]):
                        #User won
                        #keep setting the entire outcome vector to the max,
                        #ie. simulate a win of the user in the entire rounds
                        #from this point on.
                        for j in range(iMove,maxMove):
                            self.userOutcome[0,j] = self.userOutcome[0,j]+K-(iMove-1)
                            self.userOutcome[1,j] = -j
                            self.opponentOutcome[0,j]=self.opponentOutcome[0,j]-(iMove-2)
                            self.opponentOutcome[1,j] = -j
                            #exit the current game :P jeez this shit is effing ridiculous
                            break
                else:
                    opponentsMove = np.random.uniform(0.0,1.0)
                    if opponentsMove>self.p[iMove]:
                        #simulates a win by the opponent
                        #Since the opponent won, 
                        #set user's probability vector in this index to the 
                        #opponent's random probability value complement
                        self.p[iMove]=1-opponentsMove
                        
                        for j in range(iMove,maxMove):
                            self.opponentOutcome[0,j]=self.opponentOutcome[0,j]+K-(iMove-1)
                            self.opponentOutcome[1,j] = -j
                            self.userOutcome[0,j]=self.userOutcome[0,j]-(iMove-2)
                            self.userOutcome[1,j] = -j
                            #exit the current game :P jeez this shit is effing ridiculous
                            break
                        
                        
                                       
                        
                        
    def averageInGames(self,p,q, K, nGames):
        X = np.zeros(nGames)
        Y  = np.zeros(nGames)
        n = np.zeros(nGames)
        s1 = 0
        s2 = 0 
        s = 0
        
        for i in range(1,nGames):
            X[i] , Y[i], n[i], r = self.simulateGame(p,q,K)     #simulate nGames, save players profits, and the reached round
            s1 = s1 + X[i]                                      #sum of profits
            s2 = s2 + Y[i]                                      
            s = s + n[i]                                        #total number of played rounds
        s1 = s1 / nGames                                        #Average earnings in nGames
        s2 = s2 / nGames
        s = s/nGames
        return s1,s2,s
    
    
                       
    def simulateGame(self,p,q ,K):
        i=0
        endGame=False
        
        while not endGame:
            i=i+1
            r = random.random()
            if r < self.p[i]:        #player plays
                i=i+1
                r = random.random() 
                if  r < q[i]:   #Opponent plays too
                    pass
                else:           #opponents quits the game
                    X = K-(i-1) #players profit
                    Y = -(i-2)  #Opponents Profit
                    return (X,Y, i , r)
            else:               #player quits the game
                 X = -(i-2)
                 Y = K-(i-1)
                 return (X,Y, i, r)    
            if i > 1000*K:      #players reached his investment limit
                X = -(i-2)
                Y = K-(i-1)
                return (X,Y,i,r)
    
    def testSimulGame(self):
        K = 20
        p = np.full(1000000,0.9)
        
        
        q = np.full(1000000, 0.56)
        for i in range(1,20):
            X , Y , i ,r = self.simulateGame(p,q,K)
            print(X,Y,i,r)
        X,Y,N= self.averageInGames(p,q, K , 10000)
        print(X,Y,N)
             
auction = Auction()
           