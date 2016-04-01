using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ParadoxNotion;
using UnityEngine;


namespace NodeCanvas.Framework{

	///Marks the BBParameter possible to only pick values from a blackboard
	[AttributeUsage(AttributeTargets.Field)]
	public class BlackboardOnlyAttribute : Attribute{}

	///Class for Parameter Variables that allow binding to a Blackboard variable or specifying a value directly.
	[Serializable]
	abstract public class BBParameter {

		[SerializeField]
		private string _name;

		[NonSerialized]
		private IBlackboard _bb;
		[NonSerialized]
		private Variable _varRef;


		//required
		public BBParameter(){}


		///Create and return an instance of a generic BBParameter<T> with type argument provided and set to read from the specified blackboard
		public static BBParameter CreateInstance(Type t, IBlackboard bb){
			if (t == null) return null;
			var newBBParam = (BBParameter)Activator.CreateInstance( typeof(BBParameter<>).RTMakeGenericType(new Type[]{t}) );
			newBBParam.bb = bb;
			return newBBParam;
		}

		///Set the blackboard reference provided for all BBParameters and List<BBParameter> fields on the target object provided.
		public static void SetBBFields(object o, IBlackboard bb){
			var bbParams = GetObjectBBParameters(o);
			for (var i = 0; i < bbParams.Count; i++){
				bbParams[i].bb = bb;
			}
		}

		public static List<BBParameter> GetObjectBBParameters(object o){
			var bbParams = new List<BBParameter>();
			var fields = o.GetType().RTGetFields();
			for (var i = 0; i < fields.Length; i++){
				var field = fields[i];

				if (field.FieldType.RTIsSubclassOf(typeof(BBParameter))){
					var value = field.GetValue(o);
					if (value == null){
						value = Activator.CreateInstance(field.FieldType);
						field.SetValue(o, value);
					}
					bbParams.Add( (BBParameter)value );
					continue;
				}


				if (typeof(IList).RTIsAssignableFrom(field.FieldType) && !field.FieldType.IsArray && typeof(BBParameter).RTIsAssignableFrom(field.FieldType.RTGetGenericArguments()[0]) ){
					var list = field.GetValue(o) as IList;
					if (list != null){
						for (var j = 0; j < list.Count; j++){
							var bbParam = (BBParameter)list[j];
							if (bbParam == null){
								bbParam = (BBParameter)Activator.CreateInstance( field.FieldType.RTGetGenericArguments()[0] );
								list[j] = bbParam;
							}
							bbParams.Add( bbParam );
						}
					}
					continue;	
				}

				if (o is ISubParametersContainer){
					var parameters = (o as ISubParametersContainer).GetIncludeParseParameters();
					if (parameters != null){
						bbParams.AddRange( parameters );
					}
				}
			}

			return bbParams;
		}


		//Determines and gets whether the name is a path to a global bb variable
		private Variable globalVarRef{
			get
			{
				if (name != null && name.Contains("/")){
					var bbName = name.Split('/')[0];
					var varName = name.Split('/')[1];
					var globalBB = GlobalBlackboard.Find(bbName);
					if (globalBB == null){
						return null;
					}
					var globalVar = globalBB.GetVariable( varName );
					if (globalVar == null){
						return null;
					}
					return globalVar;
				}
				return null;
			}
		}

		///The Variable object reference if any.One is set after a get or set as well as well when SetBBFields is called
		///Setting the varRef also binds this parameter with that Variable.
		public Variable varRef{
			get {return _varRef;}
			set
			{
				//check for global override
				value = globalVarRef != null? globalVarRef : value;

				if (_varRef != value || value == null){
					_varRef = value;
					Bind(value);
				}
			}
		}

		///The blackboard to read/write from. Setting this also sets the variable reference
		public IBlackboard bb{
			get {return _bb;}
			set
			{
				if (_bb != value){
					_bb = value;
					varRef = value != null && !string.IsNullOrEmpty(name)? value.GetVariable(name, varType) : null;
				}
			}
		}

		///The name of the Variable to read/write from. Null if not, Empty if |NONE|.
		public string name{
			get
			{

				if (_name == null || varRef == null){
					return _name;
				}

				if (_name.Contains("/")){
					var bbName = _name.Split('/')[0];
					return bbName + "/" + varRef.name;
				}

				return varRef.name;
			}
			set
			{
				if (_name != value){
					_name = value;
					if (value != null){
						useBlackboard = true;
						if (bb != null)
							varRef = bb.GetVariable(_name, varType);
					} else {
						varRef = null;
					}
				}
			}
		}

		///Should the variable read from a blackboard variable?
		public bool useBlackboard{
			get { return name != null; }
			set
			{
				if (value == false){
					name = null;
				}
				if (value == true && name == null){
					name = string.Empty;
				}
			}
		}


		///Has the user selected |NONE| in the dropdown?
		public bool isNone{
			get {return name == string.Empty;}
		}

		///Is the final value null?
		public bool isNull{
			get	{ return objectValue == null || objectValue.Equals(null); }
		}

		///The type of the Variable reference or null if there is no Variable referenced. The returned type is for most cases the same as 'VarType'
		public Type refType{
			get {return varRef != null? varRef.varType : null;}
		}

		///The value as object type when accessing from base class
		public object value{
			get {return objectValue;}
			set {objectValue = value;}
		}

		///The raw object value
		abstract protected object objectValue{get;set;}
		///The type of the value that this BBParameter holds
		abstract public Type varType{get;}
		///Bind the BBParameter to target. Null unbinds.
		abstract protected void Bind(Variable data);

		public override string ToString(){
			if (isNone)
				return "<b>NONE</b>";
			if (useBlackboard)
				return string.Format("<b>${0}</b>", name);
			if (isNull)
				return "<b>NULL</b>";
			if (objectValue is string)
				return string.Format("<b>\"{0}\"</b>", objectValue.ToString());
			if (objectValue is IList)
				return string.Format("<b>{0}</b>", varType.FriendlyName());
			if (objectValue is IDictionary)
				return string.Format("<b>{0}</b>", varType.FriendlyName());
			if (objectValue is UnityEngine.Object)
				return string.Format("<b>{0}</b>", (objectValue as UnityEngine.Object).name );
			return string.Format("<b>{0}</b>", objectValue.ToString() );
		}
	}


	///Use BBParameter to create a parameter possible to parametrize from a blackboard variable
	[Serializable]
	public class BBParameter<T> : BBParameter{

	    public BBParameter() {}
        public BBParameter(T value) { _value = value; }

	    //delegates for Variable binding
		private Func<T> getter;
		private Action<T> setter;
		//

		[SerializeField]
		protected T _value;
		new public T value{
			get
			{
				if (getter != null)
					return getter();

				//Dynamic?
				if (name != null && bb != null){
					Bind( bb.GetVariable<T>(name) );
					if (getter != null){
						return getter();
					}
				}

				return _value;
			}
			set
			{
				if (setter != null){
					setter(value);
					return;
				}
				
				if (isNone)
					return;

				//Dynamic?
				if (name != null && bb != null){
					//setting the varRef property also binds it
					varRef = bb.SetValue(name, value);
					return;
				}

				_value = value;
			}
		}
		
		protected override object objectValue{
			get {return value;}
			set {this.value = (T)value;}
		}
		
		public override Type varType{
			get {return typeof(T);}
		}

		///Binds the BBParameter to another Variable. Null unbinds
		protected override void Bind(Variable data){
			if (data == null){
				getter = null;
				setter = null;
				if (useBlackboard){
					_value = default(T);
				}
				return;
			}

			if (!typeof(T).RTIsAssignableFrom(data.varType) && !data.varType.RTIsAssignableFrom(typeof(T)) ){
				Debug.LogWarning(string.Format("<b>BBParameter</b>: Found Variable of name '{0}' and type '{1}' on Blackboard '{2}' is not of requested type '{3}'", name, data.varType.FriendlyName(), bb.name, typeof(T).FriendlyName() ));
				return;
			}

			BindSetter(data);
			BindGetter(data);
		}

		//Bind the Getter
		void BindGetter(Variable data){
			if (data is Variable<T>){
				getter = (data as Variable<T>).GetValue;
			} else if (typeof(T).RTIsAssignableFrom(data.varType)){
				getter = ()=> { return (T)data.value; };
			}
		}

		//Bind the Setter
		void BindSetter(Variable data){
			if (data is Variable<T>){
				setter = (data as Variable<T>).SetValue;
			} else if (data.varType.RTIsAssignableFrom(typeof(T))){
				setter = (T newValue)=> { data.value = newValue; };
			}
		}


	    public static implicit operator BBParameter<T>(T value) {
	        return new BBParameter<T>{value = value};
	    }
/*
	    public static implicit operator T(BBParameter<T> param) {
	        return param.value;
	    }
*/
	}
}