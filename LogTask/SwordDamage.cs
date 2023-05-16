using NLog;

namespace LogTask
{
    internal class SwordDamage
    {
        private readonly ILogger _logger;
        private int _roll;
        private bool _isMagic;
        private bool _isFlaming;

        private const int BaseDamage = 3;
        private const int FlameDamage = 2;
        private const double MagicMultiplier = 1.75;

        public int Damage { get; private set; }

        public int Roll
        {
            get => _roll;

            set
            {
                _roll = value;

                CalculateDamage();
            }
        }

        public bool IsFlaming
        {
            get => _isFlaming;

            set
            {
                _isFlaming = value;

                _logger.Debug("Set flame damage");

                CalculateDamage();
            }
        }

        public bool IsMagic
        {
            get => _isMagic;

            set
            {
                _isMagic = value;

                _logger.Debug("Set magic damage");

                CalculateDamage();
            }
        }

        public SwordDamage()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        private void CalculateDamage()
        {
            Damage = BaseDamage;

            Damage += _isMagic ? (int)(_roll * MagicMultiplier) : _roll;

            if (_isFlaming)
            {
                Damage += FlameDamage;
            }

            _logger.Debug("Damage calculated");
        }
    }
}
